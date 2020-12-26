using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using api.ViewModels;
using App.DAL;
using Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly ILogger<DeveloperController> _logger;
        private readonly DAODeveloper _dao;

        public DeveloperController(ILogger<DeveloperController> logger, Context context)
        {
            _logger = logger;
            _dao = new DAODeveloper(context);
        }

        [HttpGet]
        public DeveloperList Get(int items=10,int page=1)
        {
            if(items < 1)
                items = 10;
            if(page < 1)
                page = 1;
            return new DeveloperList()
            {
                Items = items,
                Page = page,
                Total = _dao.Total(),
                List = _dao.List(items, page - 1).Select(dev => new DeveloperView(dev)).ToArray()
            };
        }/*
        [HttpGet]
        public DeveloperView[] TopFive()
        {
            return _dao.GetAll().Select(dev => new DeveloperView(dev)).ToArray();
        }*/
        [HttpPost]
        [Authorize]
        [Route("hour")]
        public string AddHour()
        {
            return $"{User.Identity.Name}";
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
            return Ok(new DeveloperView(temp));
        }
        [HttpPut]
        [Authorize]
        public ActionResult Put(EditDeveloperModel viewDev)
        {
            if (ModelState.IsValid)
            {
                var temp = _dao.GetById(viewDev.Id);
                if (temp == null)
                {
                    return BadRequest(new { message = $"Developer {viewDev.Id} not found!" });
                }
                if (!string.IsNullOrEmpty(viewDev.Name))
                {
                    temp.Name = viewDev.Name;
                }
                _dao.Update(temp, viewDev.Id);
                _dao.RefreshProjects(viewDev.Id,viewDev.NewProjects,viewDev.RemovedProjects);
                _dao.Save();
                return Ok(new
                {
                    developer = temp,
                    message = $"Developer {viewDev.Name} updated!"
                });
            }
           return ValidationProblem();
        }
        [HttpPost]
        public ActionResult Post(NewDeveloperModel dev)
        {
            if (ModelState.IsValid)
            {
                var developer = (Developer)dev;
                developer.CPF = dev.CPF;
                developer.Email = dev.Email;
                developer.Password = HashService.Encrypt(developer.Password);
                _dao.Add(developer);
                _dao.Save();
                return Ok(new
                {
                    developer = dev,
                    message = $"Developer {developer.Name} created!"
                });
            }
            return ValidationProblem();            
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var dev = _dao.GetById(id);
            if (dev == null)
            {
                return Ok(new { 
                    message = $"Developer {id} not found!"
                });
            }
            _dao.Delete(dev);
            _dao.Save();
            return Ok(new { 
                developer = dev,
                message = $"Developer {dev.Name} deleted!"
            });
        }
    }
}
