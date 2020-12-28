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
    public class DAODeveloper:GenericDAO<Developer>
    {
        public DAODeveloper(Context context1) : base(context1) { }

        public override Developer[] List(int items, int page)
        {
            return _context.Developers.OrderBy(prj => prj.Name).Skip(page * items).Take(items).ToArray();
        }

        public Developer GetByEmail(string email)
        {
            return _context.Developers.Where(dev => dev.Email.CompareTo(email) == 0)
                .FirstOrDefault();
        }

        public override Developer[] GetAll()
        {
            return _context.Developers.OrderBy(prj => prj.Name).ToArray();
        }
        public override Developer GetById(int id)
        {
            return _context.Developers.Where(prj => prj.Id == id)
                .FirstOrDefault();
        }
        public void RefreshProjects(int dev, int[] newPrj = null ,int[] delPrj = null)
        {
            if (delPrj != null && delPrj.Length > 0)
            {
                _context.Set<DeveloperProject>().RemoveRange(
                    _context.Set<DeveloperProject>().Where(obj => obj.DeveloperId == dev 
                        && delPrj.Contains(obj.ProjectId)).ToArray()
                );
            }
            if (newPrj != null && newPrj.Length > 0)
            {
                foreach (var prj in newPrj)
                {
                    if (_context.Set<DeveloperProject>().Where(obj => obj.DeveloperId == dev
                        && obj.ProjectId == prj).Count() == 0)
                    {
                        _context.Set<DeveloperProject>().Add(new DeveloperProject()
                        {
                            DeveloperId = dev,
                            ProjectId = prj
                        });
                    }

                }
            }
        }
        /*
        public Developer[] TopFiveWeek() {
           return _context.Developers.OrderBy(prj => prj.Name).Skip(page*items).Take(items).ToArray();
        }*/
    }
}
