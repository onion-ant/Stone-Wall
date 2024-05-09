using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoneWall.DTOs;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
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
        public async Task<ActionResult<ItemPaginationHelper>> Get([FromQuery] int genreId, [FromQuery] int atLeast, [FromQuery] string? sizeParams, [FromQuery] string? language, [FromQuery] ItemType? itemType, [FromQuery] int pageNumber = 1, [FromQuery] int offset = 6)
        {
            try
            {
                var items = await _itemsService.GetItemsAsync(pageNumber,offset,genreId,atLeast,itemType);
                var tasks = items.Items.Select(item => _tmdbService.GetItemAsync(item,language,sizeParams)).ToList();
                await Task.WhenAll(tasks);
                return items;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ExternalApiException ex)
            {
                return StatusCode(502,ex.Message);
            }
        }
        [HttpGet("{tmdbId}")]
        public async Task<ActionResult<ItemDTO>> GetDetails(int tmdbId, [FromQuery] string? sizeParams, [FromQuery] string? language)
        {
            try
            {
                var item = await _itemsService.GetDetailsAsync(tmdbId);
                await _tmdbService.GetItemAsync(item,language,sizeParams);
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
