using FluentValidation;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error message :{exceptionMessage} Time of occure :{time}", exception.Message, DateTime.UtcNow.AddHours(4));
            (string Detail, string Title, int SatusCode) details = exception switch
            {
                InternalServerException =>
                (
                exception.Message,
                exception.GetType().Name,
               context.Response.StatusCode = StatusCodes.Status500InternalServerError
               ),
                ValidationException =>(
                exception.Message,
                 exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundException => (
                 exception.Message,
                 exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status404NotFound),



                BadRequestException => (
                                exception.Message,
                                exception.GetType().Name,
                               context.Response.StatusCode = StatusCodes.Status400BadRequest
                               ),
                _ =>(
                    exception.Message,
                                exception.GetType().Name,
                               context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )

            };
            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.SatusCode,
                Instance = context.Request.Path
            };
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            if(exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidatorErrors",validationException.Errors);
            }
            await context.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
            return true;
        }
    }
}
