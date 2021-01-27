using DesafioLuby.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioLuby.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] DesenvolvedorModel model)
        {
            var service = new ServiceDesenvolvedor();
            // Recupera o usuário
            var user = service.SelectDev(model.Cpf).FirstOrDefault();

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);
            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

    }
}
