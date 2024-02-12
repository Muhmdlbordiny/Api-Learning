namespace AspNetBeginner.MiddleWare
{
    public class rateLimitingMiddleWare
    {
        private readonly RequestDelegate _next;
        private static int _counter = 0;
        private static DateTime _lastrequestDate = DateTime.Now;

        public rateLimitingMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            _counter++;
            
            if (DateTime.Now.Subtract(_lastrequestDate).Seconds > 10)
            {
                _counter = 1;
                _lastrequestDate = DateTime.Now;
                await _next(context);
            }
            else
            {
                if (_counter > 5)
                {
                    _lastrequestDate = DateTime.Now;
                    context.Response.WriteAsync("rate Limit Excuted");

                }
                else
                {
                    _lastrequestDate = DateTime.Now;
                    await _next(context);
                }
            }

            
        }
    }
}
