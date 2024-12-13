using Microsoft.AspNetCore.Http;
using Quotes.Common.AppResponse;
using Quotes.Common.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace Quotes.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            GenericResponse<string> result = null;
            if(exception is UserFriendlyException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                //result = JsonSerializer.Serialize(AppResponseFactory.BadRequest(exception.Message), new JsonSerializerOptions
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                //});
                result = AppResponseFactory.BadRequest(exception.Message);

            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //result = JsonSerializer.Serialize(AppResponseFactory.BadRequest(exception.Message), new JsonSerializerOptions
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                //});
                result = AppResponseFactory.InternalError(exception.Message);
            }

            return context.Response.WriteAsJsonAsync(result);

        }
    }
}
