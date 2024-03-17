using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Edelweiss.WebApi;

public class ExceptionMiddleware
{
    private RequestDelegate Next { get; }

    public ExceptionMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await Next(httpContext);
        }
        catch (Exception ex)
        {
            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = 500;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = CreateExceptionMessage(ex),
                Instance = string.Empty,
                Title = "Internal server error",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await httpContext.Response.WriteAsync(problemDetailsJson);
        }
    }

    private static string CreateExceptionMessage(Exception? ex)
    {
        StringBuilder sbErrorMessage = new StringBuilder();
        sbErrorMessage.AppendLine(DateTime.Now.ToString());
        while (ex != null)
        {
            sbErrorMessage.Append(
                string.Format(
                    "-> {0}{1}{2}{1}",
                    ex.Message,
                    Environment.NewLine,
                    ex.StackTrace));

            ex = ex.InnerException;
        }
        return sbErrorMessage.ToString();
    }
}
