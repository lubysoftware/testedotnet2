using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace TesteDotNet2.ProjectControlSystem.Services.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ServiceExceptionFilter : ExceptionFilterAttribute, IFilterMetadata
    {        
        public override void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext actionExecutedContext)
        {         
            //TODO Logs 

            actionExecutedContext.Exception = new Exception("Erro interno. Informe ao admnistrador do sistema"); 
        }

    }
}
