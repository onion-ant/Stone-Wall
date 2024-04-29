using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Helpers;
using StoneWall.Services;
using StoneWall.Services.Exceptions;

namespace StoneWall.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StreamingsController : ControllerBase
    {
        private readonly IStreamingServicesService _streamingServicesService;
        private readonly TmdbService _tmdbService;
        public StreamingsController(IStreamingServicesService streamingService, TmdbService tmdbService)
        {
            _streamingServicesService = streamingService;
            _tmdbService = tmdbService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StreamingServices>>> Get()
        {
            try
            {
                var streamings = await _streamingServicesService.GetStreamingsAsync();
                return streamings;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{streamingId}")]
        public async Task<ActionResult<ItemStreamingPaginationHelper>> GetItems(string streamingId, [FromQuery] int page = 1, [FromQuery] string language = "pt-BR")
        {
            try
            {
                var streamingItems = await _streamingServicesService.GetItemsAsync(streamingId, page);

                var tasks = streamingItems.ItemsStreaming.Select(item => _tmdbService.GetItemAsync(item.Item, language)).ToList();
                await Task.WhenAll(tasks);

                return streamingItems;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (LastPageException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/addons/{streamingId}")]
        public async Task<ActionResult<List<Addon>>> GetAddons(string streamingId)
        {
            try
            {
                var addons = await _streamingServicesService.GetAddonsAsync(streamingId);
                return addons;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
