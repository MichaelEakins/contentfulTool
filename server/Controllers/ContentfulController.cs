using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contentful.Core;
using Contentful.Core.Models;
using System;
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

        // ANSI color codes
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
    }
}
