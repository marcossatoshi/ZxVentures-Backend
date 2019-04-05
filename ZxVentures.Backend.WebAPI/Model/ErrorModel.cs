using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZxVentures.Backend.WebAPI.Model
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string[] Details { get; set; }
        public ErrorModel InnerError { get; set; }

        public static ErrorModel From(System.Exception e)
        {
            if (e == null)
            {
                return null;
            }
            return new ErrorModel
            {
                StatusCode = e.HResult,
                ErrorMessage = e.Message,
                InnerError = ErrorModel.From(e.InnerException)
            };
        }

        public static ErrorModel FromModelStateError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors);
            return new ErrorModel
            {
                StatusCode = 100,
                ErrorMessage = "Houve(ram) erro(s) na validação da requisição",
                Details = errors.Select(e => e.ErrorMessage).ToArray(),
            };
        }
    }
}
