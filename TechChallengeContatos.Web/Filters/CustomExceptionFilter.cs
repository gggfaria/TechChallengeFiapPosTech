using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechChallengeContatos.Web.Filters;


public class CustomExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(IWebHostEnvironment env, ILogger<CustomExceptionFilter> logger)
    {
        _env = env;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        HttpResponse response = context.HttpContext.Response;
        Exception exception = context.Exception;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.ContentType = "application/json";

        _logger.LogError(exception, "Internal server error.");

        if (_env.IsProduction())
        {
            context.Result = new JsonResult(new
            {
                message = "Error!"
            });
            return;
        }

        var result = new
        {
            message = exception.Message,
            innerException = exception.InnerException?.Message,
            stackTrace = exception.StackTrace,
        };

        context.Result = new JsonResult(result);
    }

}