using Microsoft.AspNetCore.Mvc;

namespace DesafioLuby.Controllers
{
    public class ServiceBase : ControllerBase
    {
        public string ConnectionString
        {

            get { return "Server=189.84.214.2;Database=FamesProcesso;User Id=usuariosga;Password=senha"; }
        }

    }
}
