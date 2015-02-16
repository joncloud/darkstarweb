using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class zone_settings
    {
        public int battlesolo { get; set; }
        public int battlemulti { get; set; }
        public virtual ICollection<character> home_characters { get; set; }
        public int misc { get; set; }
        public int music { get; set; }
        public string name { get; set; }
        public int navmesh { get; set; }
        public virtual ICollection<character> pos_characters { get; set; }
        public int restriction { get; set; }
        public float tax { get; set; }
        public long zoneid { get; set; }
        public long zoneip { get; set; }
        public int zoneport { get; set; }
        public int zonetype { get; set; }

        internal class Map : EntityTypeConfiguration<zone_settings>
        {
            public Map()
            {
                ToTable("zone_settings");

                HasKey(x => x.zoneid);

                HasMany(x => x.pos_characters).WithRequired(x => x.pos_zone).HasForeignKey(x => x.pos_zone_id);
                HasMany(x => x.home_characters).WithRequired(x => x.home_zone).HasForeignKey(x => x.home_zone_id);
            }
        }
    }
}
