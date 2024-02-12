using System.Diagnostics;

namespace AspNetBeginner.MiddleWare
{
    public class ProfilingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleWare> _logger;

        public ProfilingMiddleWare(RequestDelegate next,ILogger<ProfilingMiddleWare>logger)
        {
            this._next = next;
            this._logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
           await  _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"Request '{context.Request.Path}' took '{stopwatch.ElapsedMilliseconds} ms to execute'");


        }
    }
}
