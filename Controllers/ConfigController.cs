using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetBeginner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor< AttachmentOptions> _attachoptions;

        public ConfigController(IConfiguration configuration,
            IOptionsMonitor<AttachmentOptions> attachoptions)
        {
            _configuration = configuration;
            _attachoptions = attachoptions;
            var value = attachoptions.CurrentValue;
        }
        [HttpGet]
        [Route("")]
        public ActionResult GetConfig() 
        {
            Thread.Sleep(20000);
            var config = new 
            {
                AllowedHosts = _configuration["AllowedHosts"],
                DefaultConnection = _configuration["ConnectionStrings:DefaultConnection"],
                DefaultLogLevel = _configuration["Logging:LogLevel:Default"],
                TestKey = _configuration["TestKey"],
                SigningKey = _configuration["SigningKey"],
                AttachmentOption = _attachoptions.CurrentValue
            };
            return Ok(config);
        }
    }
}
