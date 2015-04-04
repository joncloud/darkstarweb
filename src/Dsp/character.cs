using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class character
    {
        public byte[] abilities { get; set; }
        public long accid { get; set; }
        public virtual account account { get; set; }
        public virtual ICollection<account_session> account_sessions { get; set; }
        public byte[] assault { get; set; }
        public virtual ICollection<auction_house> auction_houses { get; set; }
        public character_exp character_exp { get; set; }
        public virtual ICollection<character_inventory> character_inventories { get; set; }
        public virtual character_job character_job { get; set; }
        public virtual character_stats character_stats { get; set; }
        public long charid { get; set; }
        public string charname { get; set; }
        public int boundary { get; set; }
        public byte[] campaign { get; set; }
        public int gmlevel { get; set; }
        public character_location home_location { get; set; }
        public virtual zone_settings home_zone { get; set; }
        public long home_zone_id { get; set; }
        public int isnewplayer { get; set; }
        public byte[] keyitems { get; set; }
        public byte[] merits { get; set; }
        public byte[] missions { get; set; }
        public int mentor { get; set; }
        public byte nation { get; set; }
        public long playtime { get; set; }
        public character_location pos_location { get; set; }
        public virtual zone_settings pos_zone { get; set; }
        public long pos_zone_id { get; set; }
        public int pos_prevzone { get; set; }
        public byte[] quests { get; set; }
        public byte[] set_blue_spells { get; set; }
        public byte[] spells { get; set; }
        public byte[] titles { get; set; }
        public byte[] unlocked_weapons { get; set; }
        public byte[] zones { get; set; }

        internal class Map : EntityTypeConfiguration<character>
        {
            public Map()
            {
                ToTable("chars");

                HasKey(x => x.charid);

                Property(x => x.pos_location.rot).HasColumnName("pos_rot");
                Property(x => x.pos_location.x).HasColumnName("pos_x");
                Property(x => x.pos_location.y).HasColumnName("pos_y");
                Property(x => x.pos_location.z).HasColumnName("pos_z");
                Property(x => x.home_location.rot).HasColumnName("home_rot");
                Property(x => x.home_location.x).HasColumnName("home_x");
                Property(x => x.home_location.y).HasColumnName("home_y");
                Property(x => x.home_location.z).HasColumnName("home_z");
                Property(x => x.home_zone_id).HasColumnName("home_zone");
                Property(x => x.pos_zone_id).HasColumnName("pos_zone");

                HasRequired(x => x.character_exp).WithRequiredPrincipal(x => x.character);
                HasRequired(x => x.character_job).WithRequiredPrincipal(x => x.character);
                HasRequired(x => x.character_stats).WithRequiredPrincipal(x => x.Character);

                HasRequired(x => x.account).WithMany(x => x.characters).HasForeignKey(x => x.accid);
                HasRequired(x => x.pos_zone).WithMany(x => x.pos_characters).HasForeignKey(x => x.pos_zone_id);
                HasRequired(x => x.home_zone).WithMany(x => x.home_characters).HasForeignKey(x => x.home_zone_id);
                HasMany(x => x.account_sessions).WithRequired(x => x.character).HasForeignKey(x => x.charid);
                HasMany(x => x.character_inventories).WithRequired(y => y.character).HasForeignKey(y => y.charid);
            }
        }
    }
}
