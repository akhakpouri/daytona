using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Daytona.Middleware
{
    public class FriendlyExceptionsHandlingMiddleware : IMiddleware
    {
        public class ExceptionViewModel
        {
            public string Message { get; set; }
            public IDictionary Data { get; set; }
            public string Type { get; set; }
            public ExceptionViewModel InnerException { get; set; }
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var logger = context.RequestServices.GetService<ILogger<FriendlyExceptionsHandlingMiddleware>>();
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                logger?.LogError(ex, ex.Message);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.BadRequest;

            if (ex is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Forbidden;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            var output = new ExceptionViewModel();

            int i = 0;
            while (ex != null)
            {
                if (i > 0)
                {
                    output.InnerException = new ExceptionViewModel();
                    output = output.InnerException;
                }

                output.Message = ex.Message;
                output.Type = ex.GetType().Name;
                output.Data = ex.Data;

                ex = ex.InnerException;
                i++;
            }

            var result = JsonSerializer.Serialize(output, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return context.Response.WriteAsync(result);
        }
    }
}
