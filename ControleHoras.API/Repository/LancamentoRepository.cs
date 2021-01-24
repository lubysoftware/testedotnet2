using ControleHoras.API.ApiDbContexts;
using ControleHoras.API.AppModels;
using ControleHoras.API.BaseModels;
using ControleHoras.API.EntityModels;
using ControleHoras.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleHoras.API.Repository
{
    public class LancamentoRepository : RepositoryBase<LancamentoRepository>,
                                        IDataGetter<Lancamento>,
                                        IDataSetter<Lancamento>
    {
        public Lancamento Insert(Lancamento obj)
        {
            return InsertOnTable(new ApiDbContext().Lancamentos, obj);
        }
        public IList<Lancamento> PagedGet(int skip, int take)
        {
            return SkipTake(new ApiDbContext().Lancamentos, skip, take).ToList();
        }
        public Lancamento ById(int id)
        {
            return FindById(new ApiDbContext().Lancamentos, id).SingleOrDefault();
        }
        public List<RankingPosition> TopFiveOfWeekByWorkedHours()
        {
            var lastRecords = GetLastSevenDaysRecords();

            if (lastRecords.FirstOrDefault() == null)
                throw new Exception("No elements to compare");

            var result = lastRecords
                .GroupBy(c => c.IdDesenvolvedor)
                .Select(g => new RankingPosition
                {
                    IdDesenvolvedor = g.Max(w => w.IdDesenvolvedor),
                    AverageHoursWorked = g.Average(s => (s.DataFim.Value.Hour - s.DataInicio.Value.Hour))
                })
                .OrderByDescending(x => x.AverageHoursWorked)
                .Take(5)
                .ToList();

            return result;
        }

        private IQueryable<Lancamento> GetLastSevenDaysRecords()
        {
            return new ApiDbContext().Lancamentos
                .Where(x => x.DataInicio > DateTime.Now.AddDays(-7));
        }
    }
}
