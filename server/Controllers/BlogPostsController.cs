using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contentful.Core;
using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using ContenfulAPI.Models;

namespace ContenfulAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly IContentfulManagementClient _contentfulManagementClient;
        private readonly ILogger<BlogPostsController> _logger;

        public BlogPostsController(IContentfulManagementClient contentfulManagementClient, ILogger<BlogPostsController> logger)
        {
            _contentfulManagementClient = contentfulManagementClient;
            _logger = logger;
        }

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create a new blog post", Description = "Create a new blog post entry in Contentful.")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostModel model)
        {
            try
            {
                var fields = new Dictionary<string, object>
                {
                    { "title", new { en_US = model.Title } },
                    { "body", new { en_US = model.Body } },
                    { "author", new { en_US = model.Author } },
                    { "publicationDate", new { en_US = model.PublicationDate.ToString("yyyy-MM-dd") } }
                };

                if (model.Tags != null)
                {
                    fields["tags"] = new { en_US = model.Tags };
                }

                var entry = new Entry<dynamic>
                {
                    SystemProperties = new SystemProperties { Id = model.EntryId },
                    Fields = fields
                };

                // Explicitly specifying the generic type to resolve ambiguity
                await _contentfulManagementClient.CreateOrUpdateEntry<Entry<dynamic>>(entry, "blog");

                return CreatedAtAction(nameof(CreateBlogPost), new { id = entry.SystemProperties.Id }, entry);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating blog post: {Message}", ex.Message);
                return StatusCode(500, $"Error creating blog post: {ex.Message}");
            }
        }
    }
}
