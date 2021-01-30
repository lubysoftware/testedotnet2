using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api_Teste.Controllers
{
    public class DevProjetoController : Controller
    {
        public IActionResult VincularDevProjeto()
        {
            return View();
        }
    }
}
