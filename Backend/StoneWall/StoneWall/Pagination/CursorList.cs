using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace StoneWall.Pagination
{
    public sealed class CursorList<T> : List<T>
    {
        public int? Cursor {  get; set; }
        public int Limit { get; set; }

        public bool HasNext => Cursor != null;

        public CursorList(List<T> items, int limit , int? cursor)
        {
            Cursor = cursor;
            Limit = limit;

            AddRange(items);
        }

        public static async Task<CursorList<T>> ToCursorListAsync(IQueryable<T> source, int limit , int? cursor)
        {
            var items = await source.Take(limit).ToListAsync();

            return new CursorList<T>(items, limit, cursor);
        }
    }
}
