using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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
        context.Response.ContentType = "text/html";

        switch (exception)
        {
            case BookNotFoundException:
                context.Response.StatusCode = 404;
                return context.Response.SendFileAsync("wwwroot/errors/404-book.html");

            case CustomerNotFoundException:
                context.Response.StatusCode = 404;
                return context.Response.SendFileAsync("wwwroot/errors/404-customer.html");

            default:
                context.Response.StatusCode = 500;
                return context.Response.SendFileAsync("wwwroot/errors/500.html");
        }
    }

}