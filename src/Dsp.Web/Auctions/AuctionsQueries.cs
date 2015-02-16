using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Auctions
{
    public static class AuctionsQueries
    {
        public static IQueryable<AuctionHouseItem> ToAuctionHouseItems(this IQueryable<auction_house> query)
        {
            return query.Select(a => new AuctionHouseItem
            {
                BuyerName = a.buyer_name,
                DateRaw = a.date,
                Id = a.id,
                IsStack = a.stack,
                ItemId = a.itemid,
                ItemName = a.item_basic.name,
                Price = a.price,
                SaleDateRaw = a.sell_date,
                SalePrice = a.sale,
                SellerName = a.seller_name
            });
        }
    }
}