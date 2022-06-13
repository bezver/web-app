using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Middleware
{
    public class FilterExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public FilterExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ServiceException e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ServiceException e)
        {
            int statusCode = (int)(e.StatusCode != 0 ? e.StatusCode : HttpStatusCode.InternalServerError);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = statusCode,
                message = e.Message
            }));
        }
    }
}
