using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace LocoBuzz.API.Services.Filters
{
    public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var e = actionExecutedContext.Exception;
            var response = new HttpResponseMessage();
            if (e is KeyNotFoundException || e is ArgumentOutOfRangeException)
            {
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else if (e is ArgumentException)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            response.Content = new StringContent(e.Message);
            actionExecutedContext.Response = response;
        }
    }
}