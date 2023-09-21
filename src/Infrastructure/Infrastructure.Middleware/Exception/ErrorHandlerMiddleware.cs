using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Middleware.Exception;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            string errorMessage = error.Message;

            ///TODO - Log error to Monitor tool (such as splunk, NewRelic, others)

            switch (error)
            {
                case BadRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case NotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage = HttpStatusCode.InternalServerError.ToString();
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                context.Response.StatusCode,
                statusText = errorMessage
            });

            await response.WriteAsync(result);
        }
    }
}