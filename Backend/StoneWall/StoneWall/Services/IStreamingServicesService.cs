﻿using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;

namespace StoneWall.Services
{
    public interface IStreamingServicesService
    {
        public Task<IEnumerable<StreamingServices>> GetStreamingsAsync();
        public Task<IEnumerable<Addon>> GetAddonsAsync(string streamingId);
        public Task<PagedList<ItemStreaming>> GetItemsAsync(string streamingId, int pageNumber,int offset, StreamingType? streamingType, ItemParameters itemParams);
        public Task<PagedList<ItemStreaming>> CompareStreamings(string streamingExclusivo, string strimingExcluido, int pageNumber, int offset, StreamingType? streamingType, ItemParameters itemParams);
    }
}
