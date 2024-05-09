﻿using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Services.Exceptions;
using System.Linq;

namespace StoneWall.Services
{
    public class StreamingServicesService : IStreamingServicesService
    {
        private readonly StoneWallDbContext _context;
        public StreamingServicesService(StoneWallDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StreamingServices>> GetStreamingsAsync()
        {
            var streamings = await _context.Streaming_Services.AsNoTracking().ToArrayAsync();
            if (!streamings.Any())
            {
                throw new NotFoundException("Theres no registered streamings");
            }
            return streamings;
        }
        public async Task<IEnumerable<Addon>> GetAddonsAsync(string streamingId)
        {
            var addons = await _context.Addons.AsNoTracking().Where(Ad => Ad.StreamingService == streamingId).ToArrayAsync();
            if (!addons.Any())
            {
                throw new NotFoundException("This streaming has no addons");
            }
            return addons;
        }
        public async Task<ItemStreamingPaginationHelper> GetItemsAsync(string streamingId, int pageNumber, int offset, int genreId, ItemType? itemType, StreamingType? streamingType)
        {
            int totalPages = 0;
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }

            IQueryable<ItemStreaming> query = _context.Item_Streaming
            .AsNoTracking()
            .Where(Is => Is.StreamingId == streamingId);

            if (itemType != null)
            {
                query = query
               .Where(Is => Is.Item.Type == itemType);
            }
            if (streamingType != null)
            {
                query = query
               .Where(Is => Is.Type == streamingType);
            }
            if (genreId != 0)
            {
                query = query
               .Where(Is => Is.Item.Genres.Any(g => g.Id == genreId));
            }

            totalPages = await GetTotalPages(query, offset);

            if ((totalPages < pageNumber || pageNumber < 1) && totalPages != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            var streamingItems = await query
                .Select(Is => new ItemStreaming
                {
                    Item = new Item
                    {
                        TmdbId = Is.Item.TmdbId,
                        Title = Is.Item.Title,
                        OriginalTitle = Is.Item.OriginalTitle,
                        Popularity = Is.Item.Popularity,
                        Type = Is.Item.Type,
                    },
                    Type = Is.Type,
                    Link = Is.Link,
                })
                .OrderByDescending(Is => Is.Item.Popularity)
                .Skip((pageNumber - 1) * offset)
                .Take(offset)
                .ToArrayAsync();

            if (!streamingItems.Any())
            {
                throw new NotFoundException($"Theres no registered item from {streamingId}");
            }
            var response = new ItemStreamingPaginationHelper()
            {
                ItemsStreaming = streamingItems,
                LastPage = totalPages,
            };
            return response;
        }
        public async Task<ItemStreamingPaginationHelper> CompareStreamings(string streamingExclusive, string streamingExcluded, int pageNumber, int offset, int genreId, ItemType? itemType, StreamingType? streamingType)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }
            int totalPages = 0;

            var excludedTmdbIds = await _context.Item_Streaming
            .Where(Is2 => Is2.StreamingId == streamingExcluded)
            .Select(Is2 => Is2.Item.TmdbId)
            .ToArrayAsync();

            IQueryable<ItemStreaming> query = _context.Item_Streaming
            .Where(Is => Is.StreamingId == streamingExclusive && !excludedTmdbIds.Contains(Is.Item.TmdbId));

            if (itemType != null)
            {
                query = query
                .Where(Is => Is.Item.Type == itemType);
            }
            if (streamingType != null)
            {
                query = query
               .Where(Is => Is.Type == streamingType);
            }
            if (genreId != 0)
            {
                query = query
               .Where(Is => Is.Item.Genres.Any(g => g.Id == genreId));
            }

            totalPages = await GetTotalPages(query, offset);

            if ((totalPages < pageNumber || pageNumber < 1) && totalPages != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            var exclusiveItems = await query
                .Select(Is => new ItemStreaming
                {
                    Item = new Item
                    {
                        TmdbId = Is.Item.TmdbId,
                        Title = Is.Item.Title,
                        OriginalTitle = Is.Item.OriginalTitle,
                        Popularity = Is.Item.Popularity,
                        Type = Is.Item.Type
                    },
                    Type = Is.Type,
                    Link = Is.Link,
                })
                .OrderByDescending(Is => Is.Item.Popularity)
                .Skip((pageNumber - 1) * offset)
                .Take(offset)
                .ToArrayAsync();

            if (!exclusiveItems.Any())
            {
                throw new NotFoundException($"Theres no items in {streamingExclusive} that dont have in the {streamingExcluded}");
            }
            var response = new ItemStreamingPaginationHelper()
            {
                ItemsStreaming = exclusiveItems,
                LastPage = totalPages,
            };
            return response;
        }
        private async Task<int> GetTotalPages(IQueryable<ItemStreaming> query,int offset)
        {
            int count = await query.CountAsync();
            int totalPages = (count + offset - 1) / offset;

            return totalPages;
        }
    }
}
