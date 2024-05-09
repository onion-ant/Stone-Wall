using StoneWall.Entities;

namespace StoneWall.Helpers
{
    public class ItemPaginationHelper
    {
        public IEnumerable<Item>? Items { get; set; }
        public int LastPage { get; set; }
    }
}
