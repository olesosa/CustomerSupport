using CS.DOM.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CS.API.Helpers;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _logger;
    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var statusCode = context.Exception switch
        {
            ApiException apiException => apiException.Status,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var detail = context.Exception switch
        {
            _ => "Internal server error"
        };

        var problem = new ProblemDetails()
        {
            Status = statusCode,
            Detail = detail,
        };

        if (problem.Status >= StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(context.Exception, "Server error");
        }
        else if (problem.Status >= StatusCodes.Status400BadRequest)
        {
            _logger.LogError(context.Exception, "Request error");
        }
        
        var response = BuildResponse(problem);

        context.HttpContext.Response.StatusCode = response.StatusCode ?? StatusCodes.Status500InternalServerError;
        context.Result = response;
        context.ExceptionHandled = true;
    }
    
    private static ObjectResult BuildResponse(ProblemDetails problem) =>
        new ObjectResult(problem)
        {
            StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError,
            ContentTypes = new MediaTypeCollection
            {
                "application/problem+json"
            }
        };
    
}