using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Auctions
{
    public static class AuctionsQueries
    {
        public static IQueryable<AuctionHouseItem> ToAuctionHouseItems(
            this IQueryable<auction_house> query, 
            IQueryable<character> characters, 
            bool includePrice = false)
        {
            return query.Select(a => new AuctionHouseItem
            {
                BuyerName = a.buyer_name,
                BuyerId = characters.Where(c => c.charname == a.buyer_name)
                                    .Select(c => (long?)c.charid)
                                    .FirstOrDefault(),
                DateRaw = a.date,
                Id = a.id,
                IsStack = a.stack,
                ItemId = a.itemid,
                ItemName = a.item_basic.name,
                // Do not include the price unless the call indicates to do so.
                Price = includePrice ? a.price : 0,
                SaleDateRaw = a.sell_date,
                SalePrice = a.sale,
                SellerName = a.seller_name,
                SellerId = a.seller_id
            });
        }
    }
}