using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AspNetBeginner.Filters
{
    public class LogSenstivityAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
            Debug.WriteLine($"Senstivity action executed !!!!!!!");
        }
    }
}
