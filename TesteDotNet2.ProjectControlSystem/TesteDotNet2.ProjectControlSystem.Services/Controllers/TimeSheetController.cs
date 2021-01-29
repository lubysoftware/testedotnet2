using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service;
using TesteDotNet2.ProjectControlSystem.Services.ViewModel;

namespace TesteDotNet2.ProjectControlSystem.Services.Controllers
{    
    public class TimeSheetController : BaseController
    {
        private readonly ITimeSheetService timeSheetService;
        private readonly IMapper mapper;

        public TimeSheetController(ITimeSheetService timeSheetService,
            IMapper mapper)
        {
            this.timeSheetService = timeSheetService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("timesheet/add")]        
        [Authorize]
        public ActionResult<TimeSheetViewModel> Post([FromBody] TimeSheetViewModel timeSheetViewModel)
        {
            if (ValidarRequisicao())
            {
                return Ok(mapper.Map<TimeSheetViewModel>(timeSheetService.Add(mapper.Map<TimeSheet>(timeSheetViewModel))));
            }
            else
            {
                var responseError = new TimeSheetViewModel();
                responseError.Messages = ObterErroModelInvalida();
                return BadRequest(responseError);
            }
        }

        [HttpPut]
        [Route("timesheet/update")]
        [AllowAnonymous]
        public ActionResult<TimeSheetViewModel> Put([FromBody] TimeSheetViewModel timeSheetViewModel)
        {
            if (ValidarRequisicao())
            {
                return Ok(mapper.Map<TimeSheetViewModel>(timeSheetService.Update(mapper.Map<TimeSheet>(timeSheetViewModel))));
            }
            else
            {
                var responseError = new TimeSheetViewModel();
                responseError.Messages = ObterErroModelInvalida();
                return BadRequest(responseError);
            }
        }

        [HttpDelete]
        [Route("timesheet/delete")]
        [AllowAnonymous]
        public ActionResult<TimeSheetViewModel> Delete(Guid id)
        {
            var result = timeSheetService.Delete(id);
            var timesheet = new TimeSheetViewModel();
            timesheet.Messages = new List<string>();
            if (result)
            {
                timesheet.Messages.Add("Registro excluído com sucesso!");
            }
            else
            {
                timesheet.Messages.Add("Não foi possível excluir o registro");
                return BadRequest(timesheet);
            }

            return Ok(timesheet);
        }

        [HttpGet]
        [Route("timesheet/get")]
        [AllowAnonymous]
        public ActionResult<TimeSheetViewModel> Get([FromQuery] int page, int size)
        {
            return Ok(timeSheetService.Get(page, size)); 
        }

        [HttpGet]
        [Route("timesheet/getbyid")]
        [AllowAnonymous]
        public ActionResult<TimeSheetViewModel> Get([FromQuery] Guid id)
        {
            return Ok(mapper.Map<TimeSheetViewModel>(timeSheetService.GetById(id)));
        }
    }
}