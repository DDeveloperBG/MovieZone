namespace MovieZone.Infrastructure.Middleware
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionMiddleware> logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj, this.logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<CustomExceptionMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            logger.LogError(ex.Message);

            var result = JsonConvert.SerializeObject(new { StatusCode = (int)code, ErrorMessage = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
