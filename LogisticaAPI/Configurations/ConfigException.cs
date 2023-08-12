using LogisticaAPI.ApplicationServices.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaAPI.Configurations
{
    public class ConfigException : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(new { Error = context.Exception.Message });
                context.ExceptionHandled = true;
            }
        }
    }
}
