using DesafioLuby.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioLuby.Controllers
{
    public class ControllerBase : Controller
    {

        public string Token { get {
                var token = TempData["Token"].ToString();
                TempData["Token"] = token;
                return token; } }


        public bool UserLogado
        {
            get
            {
                return TokenService.ValidateToken(Token);
            }
        }


    }
}
