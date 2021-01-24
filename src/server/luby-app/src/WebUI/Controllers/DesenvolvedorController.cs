using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using luby_app.Application.Desenvolvedor.Commands.CreateDesenvolvedor;
using luby_app.Application.Desenvolvedor.Commands.DeleteDesenvolvedor;
using luby_app.Application.Desenvolvedor.Commands.UpdateDesenvolvedor;
using luby_app.Application.Desenvolvedor.Queries.GetDesenvolvedorWithPagination;
using luby_app.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;

namespace luby_app.WebUI.Controllers
{
    [Authorize]
    public class DesenvolvedorController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<DesenvolvedorDto>>> GetDesenvolvedorWithPagination([FromQuery] GetDesenvolvedorWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }
         
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateDesenvolvedorCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateDesenvolvedorCommand command)
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
            await Mediator.Send(new DeleteDesenvolvedorCommand { Id = id });

            return NoContent();
        }
    }
}
