using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contentful.Core;
using Contentful.Core.Models;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using ContenfulAPI.Models;

namespace ContenfulAPI.Controllers
{
    [ApiController]
    [Route("api/contenttypes")]
    public class ContentTypesController : ControllerBase
    {
        private readonly IContentfulManagementClient _contentfulManagementClient;
        private readonly ILogger<ContentTypesController> _logger;

        public ContentTypesController(IContentfulManagementClient contentfulManagementClient, ILogger<ContentTypesController> logger)
        {
            _contentfulManagementClient = contentfulManagementClient;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Fetch all content types", Description = "Retrieve a list of all content types in Contentful.")]
        public async Task<IActionResult> GetContentTypes()
        {
            try
            {
                var contentTypes = await _contentfulManagementClient.GetContentTypes();
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching content types: {Message}", ex.Message);
                return StatusCode(500, $"Error fetching content types: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new content type", Description = "Programmatically create a new content type in Contentful.")]
        public async Task<IActionResult> CreateContentType([FromBody] ContentTypeModel model)
        {
            try
            {
                var contentType = new ContentType
                {
                    SystemProperties = new SystemProperties { Id = model.Id },
                    Name = model.Name,
                    Description = model.Description,
                    Fields = model.Fields.Select(f => new Field
                    {
                        Id = f.Id,
                        Name = f.Name,
                        Type = f.Type,
                        Required = f.Required
                    }).ToList()
                };

                var createdContentType = await _contentfulManagementClient.CreateOrUpdateContentType(contentType);
                return CreatedAtAction(nameof(GetContentTypes), new { id = createdContentType.SystemProperties.Id }, createdContentType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating content type: {Message}", ex.Message);
                return StatusCode(500, $"Error creating content type: {ex.Message}");
            }
        }
    }
}
