using luby_app.Application.Common.Interfaces;
using luby_app.Application.Common.Models;
using luby_app.Application.DesenvolvedorHoras.Commands.Create;
using luby_app.Application.DesenvolvedorHoras.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace luby_app.WebUI.Controllers
{

    [Authorize]
    public class LacamentoHorasController : ApiControllerBase
    {
        private ICurrentUserService _currentUserService;

        public LacamentoHorasController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<DesenvolvedorHoraDto>>> GetWithPagination([FromQuery] GetDesenvolvedorHoraWithPaginationQuery query)
        {
            query.UsuarioId = _currentUserService.UserId;

            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateDesenvolvedorHoraCommand command)
        {
            command.UsuarioId = _currentUserService.UserId;

            return await Mediator.Send(command);
        }
    }
}
