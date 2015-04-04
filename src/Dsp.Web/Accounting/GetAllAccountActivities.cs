using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Dsp.Web.Accounting
{
    public class GetAllAccountActivities : IControllerAction
    {
        private readonly AccountContext _context;

        public GetAllAccountActivities(AccountContext context)
        {
            _context = context;
        }

        public PageSettings PageSettings { get; set; }
        public Uri RequestUri { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            IOrderedQueryable<Activity> query = _context.AccountActivities
                .ToActivities()
                .OrderByDescending(a => a.OccurrenceUtc);

            Page<Activity> page = await Page.FromQueryAsync(query, PageSettings);

            return page.ToMessage(RequestUri);
        }
    }
}