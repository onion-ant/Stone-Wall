﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ItemsController(IItemsService itemsService, ITmdbService tmdbService)
        {
            _itemsService = itemsService;
            _tmdbService = tmdbService;
        }
        [HttpGet]
        public async Task<ActionResult<ItemPaginationHelper>> Get([FromQuery] int genreId, [FromQuery] int atLeast, [FromQuery] ItemType? itemType, [FromQuery] int pageNumber = 1, [FromQuery] int offset = 6)
        {
            try
            {
                var items = await _itemsService.GetItemsAsync(pageNumber,offset,genreId,atLeast,itemType);
                var tasks = items.Items.Select(item => _tmdbService.GetItemAsync(item)).ToList();
                await Task.WhenAll(tasks);
                return items;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
