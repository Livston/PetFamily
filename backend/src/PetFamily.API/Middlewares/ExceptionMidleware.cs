using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PetFamily.API.Response;
using System.Net;

namespace PetFamily.API.Middlewares
{
    public class ExceptionMidleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMidleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                var responseError = new ResponseError("server.internal", ex.Message, null);
                var envelop = Envelope.Error([responseError]);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(envelop);
            }
        }
    }

    public static class ExeptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExeptionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMidleware>();
        }
    }
}
