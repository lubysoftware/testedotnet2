using Infra;
using Microsoft.EntityFrameworkCore.Storage;
using Model;
using Persistencia.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.DAL
{
    public class DAOProject : GenericDAO<Project>
    {
        public DAOProject(Context context1) : base(context1) { }

        public override Project[] GetAll()
        {
            return _context.Projects.OrderBy(prj => prj.Name).ToArray();
        }
        public override Project[] List(int items, int page)
        {
            return _context.Projects.OrderBy(prj => prj.Name).Skip(page * items).Take(items).ToArray();
        }
        public void RefreshDevelopers(DeveloperProject[] news,int project,int[] devs)
        {
            _context.Set<DeveloperProject>().RemoveRange(
                _context.Set<DeveloperProject>().Where(obj => obj.ProjectId == project && devs.Contains(obj.DeveloperId))
                .ToArray()
            );
            _context.Set<DeveloperProject>().AddRange(news);
        }
        public override Project GetById(int id)
        {
            return _context.Projects.Where(prj => prj.Id == id)
                .FirstOrDefault();
        }
    }
}
