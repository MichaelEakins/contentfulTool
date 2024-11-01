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
    [Route("api/assets")]
    public class AssetsController : ControllerBase
    {
        private readonly IContentfulClient _contentfulClient;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IContentfulClient contentfulClient, ILogger<AssetsController> logger)
        {
            _contentfulClient = contentfulClient;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Fetch all assets", Description = "Retrieve a list of all assets in Contentful.")]
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

        [HttpGet("{assetId}")]
        [SwaggerOperation(Summary = "Fetch asset by ID", Description = "Retrieve a single asset in Contentful by its ID.")]
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
    }
}
