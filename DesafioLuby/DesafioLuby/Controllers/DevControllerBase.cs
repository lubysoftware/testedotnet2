using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace DesafioLuby.Controllers
{
    public class DevControllerBase : ControllerBase
    {
        public string ConnectionString
        {

            get { return "Server=xxx;Database=xxx;User Id=xxxx;Password=xxx"; }
        }

    }
}
