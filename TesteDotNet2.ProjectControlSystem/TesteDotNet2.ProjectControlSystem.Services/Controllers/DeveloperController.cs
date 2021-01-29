using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service;
using TesteDotNet2.ProjectControlSystem.Services.ViewModel;

namespace TesteDotNet2.ProjectControlSystem.Services.Controllers
{
    public class DeveloperController : BaseController
    {
        private readonly IDeveloperService developerService;
        private readonly IMapper mapper;

        public DeveloperController(IDeveloperService developerService,
            IMapper mapper)
        {
            this.developerService = developerService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("developer/add")]
        [AllowAnonymous]
        public ActionResult<DeveloperViewModel> Post([FromBody] DeveloperViewModel developerViewModel)
        {
            if (ValidarRequisicao())
            {
                var response = mapper.Map<DeveloperViewModel>(developerService.Add(mapper.Map<Developer>(developerViewModel)));
                if (response.Messages.Any())
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            else
            {
                var responseError = new DeveloperViewModel();
                responseError.Messages = ObterErroModelInvalida();
                return BadRequest(responseError);
            }
        }

        [HttpPut]
        [Route("developer/update")]
        [AllowAnonymous]
        public ActionResult<DeveloperViewModel> Put([FromBody] DeveloperViewModel developerViewModel)
        {
            if (ValidarRequisicao())
            {
                return Ok(mapper.Map<DeveloperViewModel>(developerService.Update(mapper.Map<Developer>(developerViewModel))));
            }
            else
            {
                var responseError = new DeveloperViewModel();
                responseError.Messages = ObterErroModelInvalida();
                return BadRequest(responseError);
            }
        }

        [HttpDelete]
        [Route("developer/delete")]
        [AllowAnonymous]
        public ActionResult<DeveloperViewModel> Delete(Guid id)
        {
            var result = developerService.Delete(id);
            var developer = new DeveloperViewModel();
            developer.Messages = new List<string>();
            if (result)
            {
                developer.Messages.Add("Registro excluído com sucesso!");
            }
            else
            {
                developer.Messages.Add("Não foi possível excluir o registro");
            }

            return Ok(developer);
        }

        [HttpGet]
        [Route("developer/get")]
        [AllowAnonymous]
        public ActionResult<List<DeveloperViewModel>> Get([FromQuery] int page, int size)
        {
            return Ok(mapper.Map<List<DeveloperViewModel>>(developerService.Get(page, size)));
        }

        [HttpGet]
        [Route("developer/getbyid")]
        [AllowAnonymous]
        public ActionResult<DeveloperViewModel> Get([FromQuery] Guid id)
        {    
            return Ok(mapper.Map<DeveloperViewModel>(developerService.GetById(id)));
        }

        [HttpGet]
        [Route("developer/getReportDeveloper")]
        [AllowAnonymous]
        public ActionResult<List<ReportDeveloperResponseViewModel>> Get()
        {
            return Ok(mapper.Map<List<ReportDeveloperResponseViewModel>>(developerService.GetRankingOfHoursWorked()));
        }
    }
}