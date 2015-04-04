using System.Data.Entity.ModelConfiguration;

namespace Dsp
{
    public class character_inventory
    {
        public long bazaar { get; set; }
        public long charid { get; set; }
        public character character { get; set; }
        public byte[] extra { get; set; }
        public int itemId { get; set; }
        public byte location { get; set; }
        public long quantity { get; set; }
        public int slot { get; set; }
        public string signature { get; set; }

        internal class Map : EntityTypeConfiguration<character_inventory>
        {
            public Map()
            {
                ToTable("char_inventory");

                HasKey(x => new { x.charid, x.location, x.slot });

                Property(x => x.signature).HasMaxLength(20);

                HasRequired(x => x.character).WithMany(y => y.character_inventories).HasForeignKey(x => x.charid);
            }
        }
    }
}