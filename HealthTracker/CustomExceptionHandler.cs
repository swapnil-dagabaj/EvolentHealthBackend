using HealthTrackerSolution;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTrackerAPI
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptioAsync(context, ex);
            }
        }

        private Task HandleExceptioAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var errorMessage = new ErrorResponseData()
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                Message = ex.Message,
                Path = context.Request.Path,
            }.ToString();
            _logger.LogError($"Something went wrong inside the ${context.Request.Path}");
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
