﻿using BloggyBackend.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BloggyBackend.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ThrowException(context, ex, logger);
            }
        }

        private async Task ThrowException(HttpContext context, Exception ex, ILogger<ExceptionHandlerMiddleware> logger)
        {
            var exceptionType = ex.GetType();

            if (exceptionType == typeof(BloggyException))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest; // clients fault
                var bytes = Encoding.UTF8.GetBytes(ex.Message);
                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
            else if (exceptionType == typeof(Exception))
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError; // most likely codes fault
                logger.LogCritical(ex, ex.Message);
            } 

            await _next(context);
        }
    }
}
