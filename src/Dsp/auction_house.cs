using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class auction_house
    {
        public string buyer_name { get; set; }
        public long date { get; set; }
        public long id { get; set; }
        public virtual item_basic item_basic { get; set; }
        public long itemid { get; set; }
        public long price { get; set; }
        public long sale { get; set; }
        public long sell_date { get; set; }
        public virtual character seller { get; set; }
        public long seller_id { get; set; }
        public string seller_name { get; set; }
        public bool stack { get; set; }

        internal class Map : EntityTypeConfiguration<auction_house>
        {
            public Map()
            {
                ToTable("auction_house");

                HasKey(x => x.id);

                Property(x => x.seller_id).HasColumnName("seller");
                Property(x => x.seller_name).HasMaxLength(15);
                Property(x => x.buyer_name).HasMaxLength(15);

                HasRequired(x => x.item_basic).WithMany(x => x.auction_houses).HasForeignKey(x => x.itemid);
                HasRequired(x => x.seller).WithMany(x => x.auction_houses).HasForeignKey(x => x.seller_id);
            }
        }
    }
}
