using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoneWall.DTOs;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;
using StoneWall.Services;
using StoneWall.Services.Exceptions;

namespace StoneWall.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;
        private readonly ITmdbService _tmdbService;
        private readonly IMapper _mapper;
        public ItemsController(IItemsService itemsService, ITmdbService tmdbService, IMapper mapper)
        {
            _itemsService = itemsService;
            _tmdbService = tmdbService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> Get([FromQuery] ItemParameters itemParams, [FromQuery] TmdbParameters tmdbParams, [FromQuery] string? cursor, [FromQuery] int offset = 6)
        {
            try
            {
                var items = await _itemsService.GetItemsAsync(offset, cursor, itemParams);
                var tasks = items.Select(item => _tmdbService.GetItemAsync(item,tmdbParams)).ToList();
                await Task.WhenAll(tasks);
                var metadata = new
                {
                    items.NextCursor,
                    items.Count,
                    items.Limit,
                    items.HasNext
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                var itemsDto = items.Select(item=>_mapper.Map<ItemDTO>(item));
                return Ok(items);
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
                return StatusCode(502,ex.Message);
            }
        }
        [HttpGet("{tmdbId}")]
        public async Task<ActionResult<ItemDTO>> GetDetails(string tmdbId, [FromQuery] TmdbParameters tmdbParams)
        {
            try
            {
                var item = await _itemsService.GetDetailsAsync(tmdbId);
                await _tmdbService.GetItemAsync(item,tmdbParams);
                var itemDto = _mapper.Map<ItemDTO>(item);
                return itemDto;
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
    }
}
