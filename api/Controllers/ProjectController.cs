using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.ViewModels;
using App.DAL;
using Infra;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// Lista de projetos com paginação.
        /// </summary>
        /// <param name="items">Items exibidos por página</param>
        /// <param name="page">Numero da página</param>
        /// <returns>Lista de projetos cadastrados.</returns>
        /// <response code="200">Retorna os itens cadastrados</response>

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

        /// <summary>
        /// Projeto específico
        /// </summary>
        /// <param name="id">Id do projeto</param>
        /// <returns>dados do projeto</returns>
        /// <response code="200">Projeto encontrado</response>
        /// <response code="400">Mensagem de erro</response>
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Alterar projeto
        /// </summary>
        /// <param name="project">Objeto com os campos do projeto</param>
        /// <returns>dados do projeto</returns>
        /// <response code="200">dados do projeto removido e mensagem de confirmação</response>
        /// <response code="400">Mensagems de erro</response>
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
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
                return Ok(new ProjectResponse(temp, $"Project {project.Name} changed!"));
            }
           return ValidationProblem();
        }

        /// <summary>
        /// Criar projeto
        /// </summary>
        /// <param name="project">Objeto com os campos do projeto</param>
        /// <returns>dados do projeto</returns>
        /// <response code="200">dados do projeto removido e mensagem de confirmação</response>
        /// <response code="400">Mensagems de erro de validação</response>
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult Post(NewProjectModel project)
        {
            if (ModelState.IsValid)
            {
                var temp = new Project { Name = project.Name };
                _dao.Add(temp);
                _dao.Save();
                return Ok(new ProjectResponse(temp, $"Project {project.Name} created!"));
            }
            return ValidationProblem();            
        }

        /// <summary>
        /// Apagar projeto
        /// </summary>
        /// <param name="id">Id do projeto</param>
        /// <returns>dados do projeto</returns>
        /// <response code="200">dados do projeto removido e mensagem de confirmação</response>
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var prj = _dao.GetById(id);
            if (prj == null)
            {
                return Ok(new ProjectResponse(null, $"Project {id} not exists!"));
            }
            _dao.Delete(prj);
            _dao.Save();
            return Ok(new ProjectResponse(prj,$"Project {prj.Name} deleted!"));
        }
    }
}
