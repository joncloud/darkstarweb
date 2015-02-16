using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class item_basic
    {
        public byte aH { get; set; }
        public virtual ICollection<auction_house> auction_houses { get; set; }
        public long BaseSell { get; set; }
        public long flags { get; set; }
        public long itemid { get; set; }
        public string name { get; set; }
        public byte NoSale { get; set; }
        public string sortname { get; set; }
        public long stackSize { get; set; }
        public long subid { get; set; }

        internal class Map : EntityTypeConfiguration<item_basic>
        {
            public Map()
            {
                ToTable("item_basic");

                HasKey(x => x.itemid);

                HasMany(x => x.auction_houses).WithRequired(x => x.item_basic).HasForeignKey(x => x.itemid);
            }
        }
    }
}
