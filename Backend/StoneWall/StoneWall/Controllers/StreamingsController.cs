using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoneWall.Data;
using StoneWall.DTOs;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;
using StoneWall.Services;
using StoneWall.Services.Exceptions;
using System.Linq;

namespace StoneWall.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StreamingsController : ControllerBase
    {
        private readonly IStreamingServicesService _streamingServicesService;
        private readonly ITmdbService _tmdbService;
        private readonly IMapper _mapper;
        public StreamingsController(IStreamingServicesService streamingService, ITmdbService tmdbService,IMapper mapper)
        {
            _streamingServicesService = streamingService;
            _tmdbService = tmdbService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StreamingDTO>>> Get()
        {
            try
            {
                var streamings = await _streamingServicesService.GetStreamingsAsync();
                var streamingsDto = streamings.Select(streaming => _mapper.Map<StreamingDTO>(streaming)).ToArray();
                return Ok(streamingsDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{streamingId}")]
        public async Task<ActionResult<IEnumerable<ItemStreamingDTO>>> GetItems(string streamingId, [FromQuery] StreamingType? streamingType, [FromQuery] ItemParameters itemParams, [FromQuery] TmdbParameters tmdbParams, [FromQuery] string? cursor,[FromQuery] int offset = 6)
        {
            try
            {
                var streamingItems = await _streamingServicesService.GetItemsAsync(streamingId, cursor, offset, streamingType, itemParams);
                var tasks = streamingItems.Select(item => _tmdbService.GetItemAsync(item.Item,tmdbParams)).ToList();
                await Task.WhenAll(tasks);
                var metadata = new
                {
                    streamingItems.NextCursor,
                    streamingItems.Count,
                    streamingItems.Limit,
                    streamingItems.HasNext
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var streamingItemsDto = streamingItems.Select(sI => _mapper.Map<ItemStreamingDTO>(sI));

                return Ok(streamingItemsDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PageException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExternalApiException ex)
            {
                return StatusCode(502, ex.Message);
            }
        }
        [HttpGet("/addons/{streamingId}")]
        public async Task<ActionResult<IEnumerable<AddonDTO>>> GetAddons(string streamingId)
        {
            try
            {
                var addons = await _streamingServicesService.GetAddonsAsync(streamingId);
                var addonsDto = addons.Select(addon=>_mapper.Map<AddonDTO>(addon)).ToArray();
                return Ok(addonsDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ExternalApiException ex)
            {
                return StatusCode(502, ex.Message);
            }
        }
        [HttpGet("/compare/{streamingExclusive}-{streamingExcluded}")]
        public async Task<ActionResult<IEnumerable<ItemStreamingDTO>>> Compare(string streamingExclusive, string streamingExcluded, [FromQuery] StreamingType? streamingType, [FromQuery] TmdbParameters tmdbParams,[FromQuery] ItemParameters itemParams, [FromQuery] string? cursor, [FromQuery] int offset = 6)
        {
            try
            {
                var exclusiveItems = await _streamingServicesService.CompareStreamings(streamingExclusive, streamingExcluded, cursor, offset, streamingType, itemParams);
                var tasks = exclusiveItems.Select(item => _tmdbService.GetItemAsync(item.Item,tmdbParams)).ToList();
                await Task.WhenAll(tasks);
                var metadata = new
                {
                    exclusiveItems.NextCursor,
                    exclusiveItems.Count,
                    exclusiveItems.Limit,
                    exclusiveItems.HasNext
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                var exclusiveItemsDto = exclusiveItems.Select(exI => _mapper.Map<ItemStreamingDTO>(exI));
                return Ok(exclusiveItemsDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PageException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExternalApiException ex)
            {
                return StatusCode(502, ex.Message);
            }
        }
    }
}
