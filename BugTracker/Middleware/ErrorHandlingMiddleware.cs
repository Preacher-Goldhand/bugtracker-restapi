using BugTracker.Middleware.Custom_Exceptions;
using BugTracker.Middleware.CustomErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400; // Bad request code for the client
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404; // Not found code for the client
                await context.Response.WriteAsync(notFoundException.Message); // Generic message
            }
            catch (AlreadyExistsException alreadyExistsExcpetion)
            {
                context.Response.StatusCode = 400; // Bad request code for the client
                await context.Response.WriteAsync(alreadyExistsExcpetion.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);     // Logger level

                context.Response.StatusCode = 500; // Internal server error for the client
                await context.Response.WriteAsync("Uups...Something went wrong"); // Generic message
            }
        }
    }
}