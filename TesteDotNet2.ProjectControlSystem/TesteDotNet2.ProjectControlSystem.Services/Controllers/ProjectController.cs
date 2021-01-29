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
    public class ProjectController: BaseController
    {
        private readonly IProjectService projectService;
        private readonly IMapper mapper;

        public ProjectController(IProjectService projectService,
            IMapper mapper)
        {
            this.projectService = projectService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("project/add")]
        [AllowAnonymous]
        public ActionResult<ProjectViewModel> Post([FromBody] ProjectViewModel projectViewModel)
        {
            if (ValidarRequisicao())
            {
                return Ok(mapper.Map<ProjectViewModel>(projectService.Add(mapper.Map<Project>(projectViewModel))));
            }
            else
            {
                var responseError = new ProjectViewModel();
                responseError.Messages = ObterErroModelInvalida();
                return BadRequest(responseError);
            }
        }

        [HttpPut]
        [Route("project/update")]
        [AllowAnonymous]
        public ActionResult<ProjectViewModel> Put([FromBody] ProjectViewModel projectViewModel)
        {
            if (ValidarRequisicao())
            {
                return Ok(mapper.Map<ProjectViewModel>(projectService.Update(mapper.Map<Project>(projectViewModel))));
            }
            else
            {
                var responseError = new ProjectViewModel();
                responseError.Messages = ObterErroModelInvalida();
                return BadRequest(responseError);
            }
        }

        [HttpDelete]
        [Route("project/delete")]
        [AllowAnonymous]
        public ActionResult<ProjectViewModel> Delete(Guid id)
        {
            var result = projectService.Delete(id);
            var project = new ProjectViewModel();
            project.Messages = new List<string>();
            if (result)
            {
                project.Messages.Add("Registro excluído com sucesso!");
            }
            else
            {
                project.Messages.Add("Não foi possível excluir o registro");
                return BadRequest(project);
            }

            return Ok(project);
        }

        [HttpGet]
        [Route("project/get")]
        [AllowAnonymous]
        public ActionResult<ProjectViewModel> Get([FromQuery] int page, int size)
        {
            return Ok(projectService.Get(page, size));
        }

        [HttpGet]
        [Route("project/getbyid")]
        [AllowAnonymous]
        public ActionResult<ProjectViewModel> Get([FromQuery] Guid id)
        {
            return Ok(mapper.Map<ProjectViewModel>(projectService.GetById(id)));
        }
    }
}