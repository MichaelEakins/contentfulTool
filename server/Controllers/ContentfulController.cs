using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contentful.Core;
using Contentful.Core.Models;
using System.Threading.Tasks;

namespace ContenfulAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentfulController : ControllerBase
    {
        private readonly IContentfulClient _contentfulClient;
        private readonly ILogger<ContentfulController> _logger;

        public ContentfulController(IContentfulClient contentfulClient, ILogger<ContentfulController> logger)
        {
            _contentfulClient = contentfulClient;
            _logger = logger;

            _logger.LogInformation("ContentfulController initialized");
        }

        [HttpGet("entries")]
        public async Task<IActionResult> GetEntries()
        {
            try
            {
                // Fetch entries from Contentful
                var entries = await _contentfulClient.GetEntries<Entry<dynamic>>();
                return Ok(entries); // Return actual entries from Contentful
            }
            catch (Exception ex)
            {
                // Log and return an error if something goes wrong
                return StatusCode(500, $"Error fetching entries: {ex.Message}");
            }
        }


        // GET: api/contentful/contenttypes
        [HttpGet("contenttypes")]
        public async Task<IActionResult> GetContentTypes()
        {
            _logger.LogInformation("GetContentTypes endpoint hit");
            return Ok(new { message = "Content types endpoint reached" });
        }
    }
}
