using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dsp.Web
{
    public static class Page
    {
        async public static Task<Page<T>> FromQueryAsync<T>(IOrderedQueryable<T> query, PageSettings pageSettings)
        {
            pageSettings = pageSettings ?? new PageSettings();

            long totalCount = await query.LongCountAsync();

            int skip = pageSettings.PageNumber * pageSettings.ResultsPerPage;
            int take = pageSettings.ResultsPerPage;

            List<T> results = await query.Skip(skip)
                                         .Take(take)
                                         .ToListAsync();

            return new Page<T>(results, pageSettings.PageNumber, pageSettings.ResultsPerPage, totalCount);
        }
    }
}