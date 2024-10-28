using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using System.Threading.Tasks;

namespace ContenfulAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentfulController : ControllerBase
    {
        private readonly IContentfulClient _contentfulClient;
        private readonly IContentfulManagementClient _contentfulManagementClient;
        private readonly ILogger<ContentfulController> _logger;

        public ContentfulController(IContentfulClient contentfulClient, IContentfulManagementClient contentfulManagementClient, ILogger<ContentfulController> logger)
        {
            _contentfulClient = contentfulClient;
            _contentfulManagementClient = contentfulManagementClient;
            _logger = logger;

            _logger.LogInformation("ContentfulController initialized");
        }

        [HttpGet("entries")]
        public async Task<IActionResult> GetEntries()
        {
            try
            {
                var entries = await _contentfulClient.GetEntries<Entry<dynamic>>();
                return Ok(entries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching entries: {ex.Message}");
            }
        }

        [HttpGet("contenttypes")]
        public async Task<IActionResult> GetContentTypes()
        {
            try
            {
                var contentTypes = await _contentfulClient.GetContentTypes();
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching content types: {ex.Message}");
            }
        }

        [HttpPost("contenttype")]
        public async Task<IActionResult> CreateContentType([FromBody] ContentType contentType)
        {
            try
            {
                var createdContentType = await _contentfulManagementClient.CreateOrUpdateContentType(contentType);
                return Ok(createdContentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating content type: {ex.Message}");
            }
        }
    }
}
