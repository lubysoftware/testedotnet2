using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using luby_app.Application.Projeto.Commands.CreateProjeto;
using luby_app.Application.Projeto.Commands.DeleteProjeto;
using luby_app.Application.Projeto.Commands.UpdateProjeto;
using luby_app.Application.Projeto.Queries.GetProjetosWithPagination;
using luby_app.Application.Common.Models;
using System.Collections.Generic;
using luby_app.Application.Projeto.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;

namespace luby_app.WebUI.Controllers
{

    [Authorize]
    public class ProjetoController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjetoDto>>> GetProjetosWithPagination([FromQuery] GetProjetoWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IEnumerable<ProjetoDto>> GetAll()
        {
            return await Mediator.Send(new GetAllQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateProjetoCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProjetoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProjetoCommand { Id = id });

            return NoContent();
        }
    }
}
