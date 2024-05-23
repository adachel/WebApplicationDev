using Microsoft.AspNetCore.Mvc;

namespace ASPDepLec2.Controllers
{
    // IConfiguration – это интерфейс, предоставляемый ASP.NET Core для работы с конфигурацией в приложении.


    [ApiController]
    [Route("[controller]")]
    public class ConfigurationExampleController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigurationExampleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet(template: "getversion")]
        public ActionResult<string> GetVersion()
        {
            var version = _configuration.GetValue<string>("version:Name");
            return Ok(version);
        }



    }
}
