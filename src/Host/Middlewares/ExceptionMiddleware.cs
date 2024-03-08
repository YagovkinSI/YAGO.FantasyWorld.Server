using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Exceptions;

namespace YAGO.FantasyWorld.Server.Host.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var yagoExeption = ex as YagoException;
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = yagoExeption?.ErrorCode ?? 500;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(yagoExeption?.Message ?? "Неизвестная ошибка.");
            }
        }
    }
}
