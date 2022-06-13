using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
         IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

         public async Task InvokeAsync(HttpContext context)
         {
             try
             {
                 await next(context);
             }
             catch(Exception ex)
             {
                 logger.LogError(ex,ex.Message);
                 context.Response.ContentType = "application/json";
                 context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                 var response = environment.IsDevelopment()
                 ? new APIException ((int)HttpStatusCode.InternalServerError,ex.Message,
                   ex.StackTrace.ToString())
                 : new APIException((int)HttpStatusCode.InternalServerError);

                 var jsonOptions = new JsonSerializerOptions {PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

                 var json = JsonSerializer.Serialize(response,jsonOptions);

                 await context.Response.WriteAsync(json);
             }
         }

    }
}