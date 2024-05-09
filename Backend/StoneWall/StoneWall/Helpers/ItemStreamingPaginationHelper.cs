using StoneWall.Entities;

namespace StoneWall.Helpers
{
    public class ItemStreamingPaginationHelper
    {
        public IEnumerable<ItemStreaming>? ItemsStreaming { get; set; }
        public int LastPage { get; set; }
    }
}
