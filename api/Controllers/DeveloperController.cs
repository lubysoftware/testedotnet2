using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Services;
using api.ViewModels;
using App.DAL;
using Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly ILogger<DeveloperController> _logger;
        private readonly DAODeveloper _dao;
        private readonly Context _context;
        private static readonly HttpClient client = new HttpClient();

        public DeveloperController(ILogger<DeveloperController> logger, Context context)
        {
            _logger = logger;
            _context = context;
            _dao = new DAODeveloper(context);
        }

        /// <summary>
        /// Lista de desenvolvedores com paginação.
        /// </summary>
        /// <param name="items">Items exibidos por página</param>
        /// <param name="page">Número da página</param>
        /// <returns>Lista de desenvolvedores cadastrados.</returns>
        /// <response code="200">Retorna os itens cadastrados</response>
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
        }
        /// <summary> 
        /// Rank com as 5 maiores medias da semana
        /// </summary>
        /// <returns>lista de desenvolvedores e suas medias</returns>
        /// <response code="200">Retorna os itens cadastrados</response>
        [HttpGet]
        [Route("rank")]
        public RankModel[] TopFive()
        {
            var dom = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Sunday);

            var sat = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Saturday);
            return new DAOHour(_context).GetRank(dom,sat);
        }
        /// <summary>
        /// Lancar horas
        /// </summary>
        /// <param name="form">campos necessários para lançar a hora</param>
        /// <returns>desenvolvedor encontrado</returns>
        /// <response code="200">Mensagem da notificacao</response>
        /// <response code="400">Mensagem de erro</response>
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        [Route("hour")]
        public async Task<ActionResult> AddHour(HourRegisterModel form)
        {
            var project = new DAOProject(_context).GetById(form.ProjectId);
            if (project == null)
            {
                ModelState.AddModelError("ProjectId", "Project not exists!");
            }
            if (ModelState.IsValid)
            {
                var user = _dao.GetByEmail(User.FindFirst(ClaimTypes.Email).Value);
                var dao = new DAOHour(_context);
                var hour = new Hour()
                {
                    DeveloperId = user.Id,
                    DtBegin = form.Begin,
                    DtEnd = form.End,
                    ProjectId = form.ProjectId
                };
                dao.Add(hour);
                dao.Save();/*
                hour.Developer = null;
                var notificacao = new StringContent(JsonConvert.SerializeObject(new
                {
                    horaLancada = hour,
                    Developer = user,
                    Project = new DAOProject(_context).GetById(form.ProjectId)
                }), Encoding.UTF8, "application/json");*/

                var response = await client.PostAsync(
                    "https://run.mocky.io/v3/a1b59b8e-577d-4996-a4c5-56215907d9dd",
                    new StringContent("notificacao"));
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return Ok(responseString);
                }
                return Ok();
            }
            return ValidationProblem();
        }
        /// <summary>
        /// Desenvolvedor específico
        /// </summary>
        /// <param name="id">Id do desenvolvedor</param>
        /// <returns>desenvolvedor encontrado</returns>
        /// <response code="200">Projeto encontrado</response>
        /// <response code="400">Mensagem de erro</response>
        [ProducesResponseType(typeof(DeveloperView), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Alterar desenvolvedor
        /// </summary>
        /// <param name="viewDev">Objeto com os campos alterados</param>
        /// <returns>dados do desenvolvedor</returns>
        /// <response code="200">dados do desenvolvedor e mensagem de confirmação</response>
        /// <response code="400">Mensagems de erro</response>
        [ProducesResponseType(typeof(DeveloperResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
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
                return Ok(new DeveloperResponse(temp,$"Developer {viewDev.Name} updated!"));
            }
           return ValidationProblem();
        }

        /// <summary>
        /// Cadastrar desenvolvedor
        /// </summary>
        /// <param name="dev">Objeto com os campos</param>
        /// <returns>dados do desenvolvedor</returns>
        /// <response code="200">dados do desenvolvedor e mensagem de confirmação</response>
        /// <response code="400">Mensagems de erro</response>
        [ProducesResponseType(typeof(DeveloperResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
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
                return Ok(new DeveloperResponse(developer, $"Developer {dev.Name} created!"));
            }
            return ValidationProblem();
        }
        /// <summary>
        /// Apagar desenvolvedor
        /// </summary>
        /// <param name="id">Id do desenvolvedor</param>
        /// <returns>dados do desenvolvedor</returns>
        /// <response code="200">dados do desenvolvedor removido e mensagem de confirmação</response>
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
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
            return Ok(new DeveloperResponse(dev, $"Developer {dev.Name} deleted!"));
        }
    }
}
