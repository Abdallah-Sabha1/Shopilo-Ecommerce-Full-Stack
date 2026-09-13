using Microsoft.AspNetCore.Mvc;
using ShopiloApi.Exceptions;

namespace ShopiloApi.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
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
        catch (Exception exception)
        {
            await WriteErrorResponseAsync(context, exception);
        }
    }

    private async Task WriteErrorResponseAsync(HttpContext context, Exception exception)
    {
        int statusCode = exception switch
        {
            ProductNotFoundException => StatusCodes.Status404NotFound,
            CartNotFoundException => StatusCodes.Status404NotFound,
            CartItemNotFoundException => StatusCodes.Status404NotFound,
            OrderNotFoundException => StatusCodes.Status404NotFound,
            InsufficientStockException => StatusCodes.Status409Conflict,
            EmptyCartException => StatusCodes.Status400BadRequest,
            SeedAlreadyCompletedException => StatusCodes.Status409Conflict,
            ExternalDataException => StatusCodes.Status502BadGateway,
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "An unexpected error occurred.");
        }
        else
        {
            _logger.LogInformation(exception, "The request could not be completed.");
        }

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode == StatusCodes.Status500InternalServerError
                ? "An unexpected error occurred."
                : exception.Message,
            Detail = statusCode == StatusCodes.Status500InternalServerError
                ? "Please try again later."
                : exception.Message
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problem);
    }
}
