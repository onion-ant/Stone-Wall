using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace StoneWall.Pagination
{
    public sealed class CursorList<T> : List<T>
    {
        public string? NextCursor {  get; set; }
        public int Limit { get; set; }

        public bool HasNext => NextCursor != null;

        public CursorList(List<T> items, int limit)
        {
            Limit = limit;

            AddRange(items);
        }

        public static async Task<CursorList<T>> ToCursorListAsync(IQueryable<T> source, int limit)
        {
            var items = await source.Take(limit).ToListAsync();

            return new CursorList<T>(items, limit);
        }
    }
}
