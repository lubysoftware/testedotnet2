using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TesteDotNet2.ProjectControlSystem.Services.Filter;

namespace TesteDotNet2.ProjectControlSystem.Services.Controllers
{
    [Produces("application/json")]
    [ServiceExceptionFilter]
    public class BaseController : Controller
    {
        protected BaseController()
        {

        }

        public bool ValidarRequisicao()
        {
            if (ModelState.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected string ObterMensagemErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            string mensagensDeErro = "";
            foreach (var erro in erros)
            {
                mensagensDeErro += " - " + erro.ErrorMessage;
            }

            return mensagensDeErro;
        }

        protected List<string> ObterErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            List<string> mensagensDeErro = new List<string>();
            foreach (var erro in erros)
            {
                mensagensDeErro.Add(erro.ErrorMessage);
            }

            return mensagensDeErro;
        }
    }
}
