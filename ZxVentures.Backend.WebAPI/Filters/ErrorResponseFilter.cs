using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZxVentures.Backend.WebAPI.Model;

namespace ZxVentures.Backend.WebAPI.Filters
{
    public class ErrorResponseFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(ErrorModel.From(context.Exception)) { StatusCode = 500 };
        }
    }
}
