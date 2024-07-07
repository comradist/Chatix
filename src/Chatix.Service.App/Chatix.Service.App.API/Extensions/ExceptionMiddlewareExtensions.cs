using Chatix.Libs.Core.Contracts.Logger;
using Chatix.Libs.Core.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Chatix.Service.App.API.Extensions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IHostEnvironment hostEnvironment;
    private readonly ILoggerManager loggerManager;

    public GlobalExceptionHandler(IHostEnvironment hostEnvironment, ILoggerManager loggerManager)
    {
        this.hostEnvironment = hostEnvironment;
        this.loggerManager = loggerManager;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        loggerManager.LogError(exception.ToString());

        var problemDetails = CreateProblemDetails(httpContext, exception);
        var serializeProblemDetails = JsonConvert.SerializeObject(problemDetails);
        await httpContext.Response.WriteAsync(serializeProblemDetails, cancellationToken: cancellationToken);
        return true;
    }

    private ProblemDetails CreateProblemDetails(in HttpContext httpContext, in Exception exception)
    {
        var statusCode = httpContext.Response.StatusCode;
        var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
        var type = exception.GetType().Name;

        var problemDetails = new ProblemDetails
        {
            Type = type,
            Status = statusCode,
            Title = reasonPhrase,
        };

        if (!hostEnvironment.IsDevelopment())
        {
            return problemDetails;
        }
        problemDetails.Detail = exception.Message;
        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
        //problemDetails.Extensions["data"] = exception.Data;
        problemDetails.Instance = string.Concat(httpContext.Request.Host.Value, httpContext.Request.Path.Value);
        return problemDetails;
    }
}
