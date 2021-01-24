using ControleHoras.API.AppModels;
using ControleHoras.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ControleHoras.API.BaseModels
{
    public abstract class ApiControllerBase : ControllerBase
    {
        public int IdDesenvolvedor => int.Parse(User.GetClaim(ApiClaimTypes.IdDesenvolvedor));
    }
}
