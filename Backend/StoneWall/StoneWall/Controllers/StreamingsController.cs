using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
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
        private readonly ITmdbService _tmdbService;
        public StreamingsController(IStreamingServicesService streamingService, ITmdbService tmdbService)
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
        public async Task<ActionResult<ItemStreamingPaginationHelper>> GetItems(string streamingId, [FromQuery] int genreId, [FromQuery] ItemType? itemType, [FromQuery] StreamingType? streamingType, [FromQuery] int pageNumber = 1, [FromQuery] int offset = 6)
        {
            try
            {
                var streamingItems = await _streamingServicesService.GetItemsAsync(streamingId, pageNumber, offset, genreId, itemType, streamingType);

                var tasks = streamingItems.ItemsStreaming.Select(item => _tmdbService.GetItemAsync(item.Item)).ToList();
                await Task.WhenAll(tasks);

                return streamingItems;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PageException ex)
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
        [HttpGet("/compare/{streamingExclusive}-{streamingExcluded}")]
        public async Task<ActionResult<ItemStreamingPaginationHelper>> Compare(string streamingExclusive, string streamingExcluded, [FromQuery] int genreId, [FromQuery] ItemType? itemType, [FromQuery] StreamingType? streamingType, [FromQuery] int pageNumber = 1, [FromQuery] int offset = 6)
        {
            try
            {
                var exclusiveItems = await _streamingServicesService.CompareStreamings(streamingExclusive, streamingExcluded, pageNumber, offset, genreId, itemType, streamingType);
                var tasks = exclusiveItems.ItemsStreaming.Select(item => _tmdbService.GetItemAsync(item.Item)).ToList();
                await Task.WhenAll(tasks);
                return exclusiveItems;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PageException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
