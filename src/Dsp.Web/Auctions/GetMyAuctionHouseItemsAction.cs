using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Dsp.Web.Auctions
{
    public class GetMyAuctionHouseItemsAction : IControllerAction
    {
        private readonly DspContext _context;

        public GetMyAuctionHouseItemsAction(DspContext context)
        {
            _context = context;
        }

        public long OwnerAccountId { get; set; }
        public PageSettings PageSettings { get; set; }
        public Uri RequestUri { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            IOrderedQueryable<AuctionHouseItem> query = _context.accounts.Where(a => a.id == OwnerAccountId)
                .SelectMany(a => a.characters)
                .SelectMany(c => c.auction_houses)
                .ToAuctionHouseItems()
                .OrderByDescending(i => i.Id);

            Page<AuctionHouseItem> page = await Page.FromQueryAsync(query, PageSettings);

            return page.ToMessage(RequestUri);
        }
    }
}