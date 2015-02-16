using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Auctions
{
    public class AuctionHouseItem
    {
        public string BuyerName { get; set; }
        public DateTime? Date { get; set; }

        [JsonIgnore]
        public long DateRaw
        {
            get { return TypeConverters.ToRaw(Date); }
            set { Date = TypeConverters.FromRaw(value); }
        }

        public long Id { get; set; }

        public bool IsStack { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public long Price { get; set; }
        public DateTime? SaleDate { get; set; }

        [JsonIgnore]
        public long SaleDateRaw
        {
            get { return TypeConverters.ToRaw(SaleDate); }
            set { SaleDate = TypeConverters.FromRaw(value); }
        }

        public long SalePrice { get; set; }

        public string SellerName { get; set; }
    }
}