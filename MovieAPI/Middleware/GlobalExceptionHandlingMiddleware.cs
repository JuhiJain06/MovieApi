﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MovieAPI.Exceptions;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private const string GenericErrorMessage = "An unexpected error occurred.";

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
            catch (Exception ex)
            {
                // Once the response has started we can no longer change the status code or body.
                if (context.Response.HasStarted)
                {
                    _logger.LogError(ex, "Unhandled exception after the response started on {Path}", context.Request.Path);
                    throw;
                }

                var (statusCode, message) = ex switch
                {
                    NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
                    UnauthorizedException => (StatusCodes.Status401Unauthorized, ex.Message),
                    BadRequestException => (StatusCodes.Status400BadRequest, ex.Message),
                    // Never expose internal details (SQL errors, stack info, etc.) to the client.
                    _ => (StatusCodes.Status500InternalServerError, GenericErrorMessage)
                };

                if (statusCode >= StatusCodes.Status500InternalServerError)
                {
                    _logger.LogError(ex, "Unhandled exception on {Method} {Path}", context.Request.Method, context.Request.Path);
                }
                else
                {
                    _logger.LogWarning("{StatusCode} on {Method} {Path}: {Message}", statusCode, context.Request.Method, context.Request.Path, ex.Message);
                }

                context.Response.Clear();
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = statusCode,
                    Message = message
                });
            }
        }
    }
}
