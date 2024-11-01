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
    [Route("api/entries")]
    public class EntriesController : ControllerBase
    {
        private readonly IContentfulClient _contentfulClient;
        private readonly ILogger<EntriesController> _logger;

        public EntriesController(IContentfulClient contentfulClient, ILogger<EntriesController> logger)
        {
            _contentfulClient = contentfulClient;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Fetch all entries", Description = "Retrieve a list of all entries in Contentful.")]
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

        [HttpGet("{entryId}")]
        [SwaggerOperation(Summary = "Fetch entry by ID", Description = "Retrieve a single entry in Contentful by its ID.")]
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
    }
}
