using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContentfulAPI.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
            _logger.LogInformation("TestController initialized");
        }

        [HttpGet]
        public IActionResult GetTest()
        {
            _logger.LogInformation("GetTest endpoint hit");
            return Ok("Test endpoint is working.");
        }
    }
}
