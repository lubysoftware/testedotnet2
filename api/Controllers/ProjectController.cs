using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.ViewModels;
using App.DAL;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly DAOProject _dao;

        public ProjectController(ILogger<ProjectController> logger, Context context)
        {
            _logger = logger;
            _dao = new DAOProject(context);
        }

        [HttpGet]
        public ProjectList Get(int items=10,int page=1)
        {
            if(items < 1)
                items = 10;
            if(page < 1)
                page = 1;
            return new ProjectList()
            {
                Items = items,
                Page = page,
                Total = _dao.Total(),
                List = _dao.List(items, page - 1)
            }; ;
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            var temp = _dao.GetById(id);
            if (temp == null)
            {
                return BadRequest(new { message = $"Developer {id} not found!" });
            }
            return Ok(temp);
        }
        [HttpPut]
        public ActionResult Put(Project project)
        {
            var temp = _dao.GetById(project.Id);
            if (temp == null)
            {
                return BadRequest(new { message = $"Project {project.Id} not found!" });
            }
            if (ModelState.IsValid)
            {
                _dao.Update(project, project.Id);
                _dao.Save();
                return Ok(new
                {
                    project = temp,
                    message = $"Project {temp.Name} updated!"
                });
            }
           return ValidationProblem();
        }
        [HttpPost]
        public ActionResult Post(NewProjectModel project)
        {
            if (ModelState.IsValid)
            {
                var temp = new Project { Name = project.Name };
                _dao.Add(temp);
                _dao.Save();
                return Ok(new
                {
                    project = temp,
                    message = $"Project {temp.Name} created!"
                });
            }
            return ValidationProblem();
            
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var prj = _dao.GetById(id);
            if (prj == null)
            {
                return Ok(new { 
                    message = $"Project {id} not found!"
                });
            }
            _dao.Delete(prj);
            _dao.Save();
            return Ok(new
            {
                developer = prj,
                message = $"PRoject {prj.Name} deleted!"
            });
        }
    }
}
