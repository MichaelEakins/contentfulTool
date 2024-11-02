using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contentful.Core;
using Contentful.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using ContenfulAPI.Models;

namespace ContenfulAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentTypesController : ControllerBase
    {
        private readonly IContentfulManagementClient _contentfulManagementClient;
        private readonly ILogger<ContentTypesController> _logger;

        public ContentTypesController(IContentfulManagementClient contentfulManagementClient, ILogger<ContentTypesController> logger)
        {
            _contentfulManagementClient = contentfulManagementClient;
            _logger = logger;
        }

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create a new content type", Description = "Programmatically create a new content type in Contentful.")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateContentType([FromBody] ContentTypeModel model)
        {
            try
            {
                var contentType = new ContentType
                {
                    SystemProperties = new SystemProperties { Id = model.Id },
                    Name = model.Name,
                    Description = model.Description,
                    Fields = new List<Field>()
                };

                foreach (var field in model.Fields)
                {
                    contentType.Fields.Add(new Field
                    {
                        Id = field.Id,
                        Name = field.Name,
                        Type = field.Type,
                        Required = field.Required,
                    });
                }

                // Correct the second argument by passing a string as the expected value
                var createdContentType = await _contentfulManagementClient.CreateOrUpdateContentType(contentType, "master");

                return CreatedAtAction(nameof(CreateContentType), new { id = createdContentType.SystemProperties.Id }, createdContentType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating content type: {Message}", ex.Message);
                return StatusCode(500, $"Error creating content type: {ex.Message}");
            }
        }
    }
}
