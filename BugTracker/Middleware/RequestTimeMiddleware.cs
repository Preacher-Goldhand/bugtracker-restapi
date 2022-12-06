using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopWatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();

            var elapsedTime = _stopWatch.ElapsedMilliseconds;
            if (elapsedTime / 1000 > 100)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedTime} ms";
                _logger.LogInformation(message);
            }
        }
    }
}