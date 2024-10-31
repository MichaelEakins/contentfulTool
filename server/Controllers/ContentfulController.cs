using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contentful.Core;
using Contentful.Core.Models;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

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
            _logger.LogInformation("ContentfulController initialized.");
        }

        [HttpGet("contenttypes")]
        [SwaggerOperation(Summary = "Fetch all content types", Description = "Retrieve a list of all content types in Contentful.", Tags = new[] { "Content Types" })]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetContentTypes()
        {
            try
            {
                var contentTypes = await _contentfulClient.GetContentTypes();
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching content types: {Message}", ex.Message);
                return StatusCode(500, $"Error fetching content types: {ex.Message}");
            }
        }

        [HttpGet("entries")]
        [SwaggerOperation(Summary = "Fetch all entries", Description = "Retrieve a list of all entries in Contentful.", Tags = new[] { "Entries" })]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEntries()
        {
            try
            {
                var entries = await _contentfulClient.GetEntries<Entry<dynamic>>();
                return Ok(entries);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching entries: {Message}", ex.Message);
                return StatusCode(500, $"Error fetching entries: {ex.Message}");
            }
        }

        [HttpGet("entries/{entryId}")]
        [SwaggerOperation(Summary = "Fetch entry by ID", Description = "Retrieve a single entry in Contentful by its ID.", Tags = new[] { "Entries" })]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEntryById(string entryId)
        {
            try
            {
                var entry = await _contentfulClient.GetEntry<Entry<dynamic>>(entryId);
                return Ok(entry);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching entry by ID: {Message}", ex.Message);
                return StatusCode(500, $"Error fetching entry by ID: {ex.Message}");
            }
        }

        [HttpGet("assets")]
        [SwaggerOperation(Summary = "Fetch all assets", Description = "Retrieve a list of all assets in Contentful.", Tags = new[] { "Assets" })]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAssets()
        {
            try
            {
                var assets = await _contentfulClient.GetAssets();
                return Ok(assets);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching assets: {Message}", ex.Message);
                return StatusCode(500, $"Error fetching assets: {ex.Message}");
            }
        }

        [HttpGet("assets/{assetId}")]
        [SwaggerOperation(Summary = "Fetch asset by ID", Description = "Retrieve a single asset in Contentful by its ID.", Tags = new[] { "Assets" })]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAssetById(string assetId)
        {
            try
            {
                var asset = await _contentfulClient.GetAsset(assetId);
                return Ok(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching asset by ID: {Message}", ex.Message);
                return StatusCode(500, $"Error fetching asset by ID: {ex.Message}");
            }
        }

        [HttpPost("contenttypes")]
        [SwaggerOperation(Summary = "Create a new content type", Description = "Programmatically create a new content type in Contentful.", Tags = new[] { "Content Types" })]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateContentType([FromBody] ContentType contentType)
        {
            try
            {
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
