using Microsoft.AspNetCore.Http;
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
    }
}
