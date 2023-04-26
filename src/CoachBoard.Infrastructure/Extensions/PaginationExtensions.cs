using CoachBoard.Core.Entities.Base;
using CoachBoard.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Extensions;

public static class PaginationExtensions
{
    public static async Task<PaginationResult<T>> GetPaged<T>(
        this IQueryable<T> query,
        int page,
        int pageSize) where T : BaseEntity
    {
        var result = new PaginationResult<T>
        {
            Page = page,
            PageSize = pageSize,
            ItemsCount = await query.CountAsync()
        };

        var pageCount = (double)result.ItemsCount / pageSize;
        result.TotalPages = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;

        result.Data = await query
            .OrderBy(t => t.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return result;
    }
}