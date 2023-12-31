using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Talabat.Helper
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                var ExceptionErrorResponse = env.IsDevelopment() ?
                    new ExceptionErrorResponse(500, ex.Message, ex.StackTrace.ToString())
                    :
                    new ExceptionErrorResponse(500);
                var json = JsonSerializer.Serialize(ExceptionErrorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
