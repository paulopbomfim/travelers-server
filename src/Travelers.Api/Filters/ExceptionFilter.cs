using Travelers.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Travelers.Communication.Responses;
using Travelers.Exception.ExceptionBase;

namespace Travelers.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is TravelersException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        var travelersException = (TravelersException)context.Exception;
        context.HttpContext.Response.StatusCode = travelersException.StatusCode;
        
        var errorResponse = new ErrorResponse(travelersException.GetErrors());
        context.Result = new ObjectResult(errorResponse);
    }

    private static void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ErrorResponse(ErrorMessagesResource.UNKNOWN_ERROR);
        
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}