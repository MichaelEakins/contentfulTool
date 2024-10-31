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

        private const string Green = "\u001b[32m";
        private const string Red = "\u001b[31m";
        private const string Yellow = "\u001b[33m";
        private const string Reset = "\u001b[0m";

        public ContentfulController(IContentfulClient contentfulClient, IContentfulManagementClient contentfulManagementClient, ILogger<ContentfulController> logger)
        {
            _contentfulClient = contentfulClient;
            _contentfulManagementClient = contentfulManagementClient;
            _logger = logger;
            _logger.LogInformation($"{Green}ContentfulController initialized.{Reset}");
        }

        [HttpGet("contenttypes")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Fetch all content types", Description = "Retrieve a list of all content types in Contentful.")]
        [ApiExplorerSettings(GroupName = "Content Types")]
        public async Task<IActionResult> GetContentTypes()
        {
            _logger.LogInformation($"{Yellow}GetContentTypes endpoint hit.{Reset}");
            try
            {
                var contentTypes = await _contentfulClient.GetContentTypes();
                _logger.LogInformation($"{Green}Successfully fetched content types.{Reset}");
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Red}Error fetching content types: {ex.Message}{Reset}");
                return StatusCode(500, $"Error fetching content types: {ex.Message}");
            }
        }

        [HttpGet("entries")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Fetch all entries", Description = "Retrieve a list of all entries in Contentful.")]
        [ApiExplorerSettings(GroupName = "Entries")]
        public async Task<IActionResult> GetEntries()
        {
            _logger.LogInformation($"{Yellow}GetEntries endpoint hit.{Reset}");
            try
            {
                var entries = await _contentfulClient.GetEntries<Entry<dynamic>>();
                _logger.LogInformation($"{Green}Successfully fetched entries.{Reset}");
                return Ok(entries);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Red}Error fetching entries: {ex.Message}{Reset}");
                return StatusCode(500, $"Error fetching entries: {ex.Message}");
            }
        }

        [HttpGet("entries/{entryId}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Fetch entry by ID", Description = "Retrieve a single entry in Contentful by its ID.")]
        [ApiExplorerSettings(GroupName = "Entries")]
        public async Task<IActionResult> GetEntryById(string entryId)
        {
            _logger.LogInformation($"{Yellow}GetEntryById endpoint hit for ID: {entryId}{Reset}");
            try
            {
                var entry = await _contentfulClient.GetEntry<Entry<dynamic>>(entryId);
                _logger.LogInformation($"{Green}Successfully fetched entry by ID.{Reset}");
                return Ok(entry);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Red}Error fetching entry by ID: {ex.Message}{Reset}");
                return StatusCode(500, $"Error fetching entry by ID: {ex.Message}");
            }
        }

        [HttpGet("assets")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Fetch all assets", Description = "Retrieve a list of all assets in Contentful.")]
        [ApiExplorerSettings(GroupName = "Assets")]
        public async Task<IActionResult> GetAssets()
        {
            _logger.LogInformation($"{Yellow}GetAssets endpoint hit.{Reset}");
            try
            {
                var assets = await _contentfulClient.GetAssets();
                _logger.LogInformation($"{Green}Successfully fetched assets.{Reset}");
                return Ok(assets);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Red}Error fetching assets: {ex.Message}{Reset}");
                return StatusCode(500, $"Error fetching assets: {ex.Message}");
            }
        }

        [HttpGet("assets/{assetId}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Fetch asset by ID", Description = "Retrieve a single asset in Contentful by its ID.")]
        [ApiExplorerSettings(GroupName = "Assets")]
        public async Task<IActionResult> GetAssetById(string assetId)
        {
            _logger.LogInformation($"{Yellow}GetAssetById endpoint hit for ID: {assetId}{Reset}");
            try
            {
                var asset = await _contentfulClient.GetAsset(assetId);
                _logger.LogInformation($"{Green}Successfully fetched asset by ID.{Reset}");
                return Ok(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Red}Error fetching asset by ID: {ex.Message}{Reset}");
                return StatusCode(500, $"Error fetching asset by ID: {ex.Message}");
            }
        }
    }
}
