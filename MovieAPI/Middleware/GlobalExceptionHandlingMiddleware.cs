using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MovieAPI.Exceptions;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, " An Error has occured.");
                context.Response.ContentType = "application/json";

                switch (ex)
                {
                    case NotFoundException:
                        context.Response.StatusCode=StatusCodes.Status404NotFound; break;

                    case UnauthorizedException:
                        context.Response.StatusCode=StatusCodes.Status401Unauthorized; break;

                    case BadRequestException:
                        context.Response.StatusCode=StatusCodes.Status400BadRequest; break;

                    default:
                        context.Response.StatusCode =StatusCodes.Status500InternalServerError; break;
                }

                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }

        }
    }
}
