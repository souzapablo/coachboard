using CoachBoard.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Application.Extensions;
public static class PaginationExtensions
{
    public static async Task<PaginatedResult<T>> CreatePaginationAsync<T>(this IQueryable<T> data, int page, int pageSize)
    {
        var count = await data.CountAsync();

        var list = await data
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<T>(list, page, pageSize, count);
    }
}
