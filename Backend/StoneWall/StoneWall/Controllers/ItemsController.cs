using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoneWall.DTOs.ItemCatalogDTOs;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Extensions.Mappings;
using StoneWall.DTOs.ExternalApiDTOs;
using StoneWall.Pagination;
using StoneWall.Services;
using StoneWall.Services.Exceptions;
using System.Web;

namespace StoneWall.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;
        private readonly ITmdbService _tmdbService;
        public ItemsController(IItemsService itemsService, ITmdbService tmdbService)
        {
            _itemsService = itemsService;
            _tmdbService = tmdbService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCatalogDTO>>> Get([FromQuery] ItemParameters itemParams, [FromQuery] TmdbParameters tmdbParams, [FromQuery] string? cursor, [FromQuery] int offset = 6)
        {
            try
            {
                var items = await _itemsService.GetItemsAsync(offset, cursor, itemParams);
                var tasks = items.Select(item => _tmdbService.GetItemAsync(item,tmdbParams)).ToList();
                await Task.WhenAll(tasks);
                var itemsDto = items.ToItemPaginationDTO();
                return Ok(itemsDto);
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
        public async Task<ActionResult<ItemCatalogDTO>> GetDetails(string tmdbId, [FromQuery] TmdbParameters tmdbParams)
        {
            try
            {
                tmdbId = HttpUtility.UrlDecode(tmdbId);
                var item = await _itemsService.GetDetailsAsync(tmdbId);
                await _tmdbService.GetItemAsync(item,tmdbParams);
                var itemDto =item.ToItemDTO();
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
