using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Dsp.Web.Auctions
{
    public class GetAllAuctionHouseItemsAction : IControllerAction
    {
        private readonly DspContext _context;

        public GetAllAuctionHouseItemsAction(DspContext context)
        {
            _context = context;
        }

        public PageSettings PageSettings { get; set; }
        public Uri RequestUri { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            IOrderedQueryable<AuctionHouseItem> query = _context.auction_houses
                .ToAuctionHouseItems()
                .OrderByDescending(i => i.Id);

            Page<AuctionHouseItem> page = await Page.FromQueryAsync(query, PageSettings);

            return page.ToMessage(RequestUri);
        }
    }
}