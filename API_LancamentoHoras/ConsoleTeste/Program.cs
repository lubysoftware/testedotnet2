using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            DbLancamentoHorasEntities db = new DbLancamentoHorasEntities();

            //Semana semana = new Semana(DateTime.Now);
            Semana semana = new Semana(DateTime.Parse("2021-01-09"));

            var query = db.LancamentoHoras.Where(x => x.DataInicial >= semana.Inicial && x.DataFinal <= semana.Final);

            List<DesenvolvedorRanking> desenvolvedorRankings = new List<DesenvolvedorRanking>();

            foreach (var item in query.GroupBy(x => x.Desenvolvedor).ToList())
            {
                var lancamentoHorasDesenv = query.Where(x => x.DesenvolvedorId == item.Key.Id).ToList();
                desenvolvedorRankings.Add(new DesenvolvedorRanking(item.Key, SomaHoras(lancamentoHorasDesenv)));
            };

            foreach (var item in desenvolvedorRankings.OrderByDescending(x => x.HorasSoma).Take(5))
            {
                Console.WriteLine("{0} {1} - {2}", item._Desenvolvedor.Id, item._Desenvolvedor.Nome, item.HorasSoma);
            }

            Console.ReadKey();
        }

        private static TimeSpan SomaHoras(ICollection<LancamentoHoras> lancamentoHoras)
        {
            TimeSpan soma = new TimeSpan();

            foreach (var item in lancamentoHoras)
            {
                TimeSpan sub = item.DataFinal - item.DataInicial;
                soma = soma + sub;
            }

            return soma;
        }

        public class Semana
        {
            public DateTime Inicial { get; set; }
            public DateTime Final { get; set; }

            public Semana(DateTime Data)
            {                
                int DiaSemana = (int)Data.DayOfWeek;

                DateTime ini = Data.AddDays(-DiaSemana);
                DateTime fim = Data.AddDays(6 - DiaSemana);

                this.Inicial = new DateTime(ini.Year, ini.Month, ini.Day, 0, 0, 0);
                this.Final = new DateTime(fim.Year, fim.Month, fim.Day, 23, 59, 59);
            }
        }

        class DesenvolvedorRanking
        {
            public Desenvolvedor _Desenvolvedor { get; set; }
            public TimeSpan HorasSoma { get; set; }

            public DesenvolvedorRanking(Desenvolvedor _Desenvolvedor, TimeSpan HorasSoma)
            {
                this._Desenvolvedor = _Desenvolvedor;
                this.HorasSoma = HorasSoma;

            }
        }
    }
}
