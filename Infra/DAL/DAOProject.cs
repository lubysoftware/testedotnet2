using Infra;
using Microsoft.EntityFrameworkCore.Storage;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.DAL
{
    public class DAOProject
    {
        protected readonly Context _context;

        public DAOProject(Context context)
        {
            _context = context;
        }
        public Project[] Get(int items,int page) {
            return _context.Projects.OrderBy(prj => prj.Name).Skip(page*items).Take(items).ToArray();
        }
        public Project GetById(int id) {
            return _context.Projects.Where(prj => prj.Id == id).FirstOrDefault();
        }
        public void Add(Project obj)
        {
            try
            {
                _context.Projects.Add(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Update(Project newProject, int id)
        {
            try
            {
                Project old = GetById(id);
                _context.Entry(old).CurrentValues.SetValues(newProject);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual void Delete(Project project)
        {
            try
            {
                _context.Projects.Remove(project);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IDbContextTransaction BeginTtransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
