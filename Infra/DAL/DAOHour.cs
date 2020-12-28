using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Model;
using Persistencia.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.DAL
{
    public class DAOHour:GenericDAO<Hour>
    {
        public DAOHour(Context context1) : base(context1) { }

        public override Hour[] GetAll()
        {
            return _context.Hours.OrderBy(hr => hr.DtBegin).ToArray();
        }
        public override Hour GetById(int id)
        {
            return _context.Hours.Where(hr => hr.Id == id)
                .FirstOrDefault();
        }

        public override Hour[] List(int items, int page)
        {
            return _context.Hours.OrderBy(hr => hr.DtBegin).Skip(page * items).Take(items).ToArray();
        }

        public RankModel[] GetRank(DateTime beginWeek,DateTime endWeek)
        {
            var list = _context.Hours.Where(hr => hr.DtBegin >= beginWeek && hr.DtEnd <= endWeek)
                .Select(h => new TimeCalculated
            {
                Developer = h.DeveloperId,
                Project = h.ProjectId,
                DateDiff = h.DtEnd.Subtract(h.DtBegin).TotalHours
            }).ToArray();
            return list.GroupBy(h => h.Developer).Select(r => new RankModel{ 
                DeveloperId = r.Key,
                Name = _context.Developers.Where(dev => dev.Id == r.Key).Select(dev => dev.Name).FirstOrDefault(),
                Average = r.Sum(a =>a.DateDiff)/ r.Select(e => e.Project).Distinct().Count()
            }).OrderByDescending(h => h.Average).Take(5).ToArray();
        }
    }

    public class TimeCalculated
    {
        public int Developer { get; set; }
        public int Project { get; set; }
        public double DateDiff { get; set; }
    }
}

