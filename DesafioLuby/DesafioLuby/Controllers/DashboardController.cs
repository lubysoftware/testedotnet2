using DesafioLuby.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioLuby.Controllers
{
    public class DashboardController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(string Cpf)
        {
            var service = new ServiceDesenvolvedor();
            var user = service.SelectDev(Cpf).FirstOrDefault();

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            var _ = new
            {
                user = user,
                token = token
            };

            TempData["Token"] = token;

            return View("Dashboard");
        }

        [HttpPost]
        [Route("horas")]
        public async Task<ActionResult<dynamic>> horas(HorasLancamentoModel model)
        {

            var Sucess = new
            {
                Sucess = true,
            };
            if (!UserLogado) {
                await new ServiceHorasLancamento().InsertAsync(model);
            }

            return Sucess.Sucess;
        }



    }
}
