using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class character_stats
    {
        public string bazaar_message { get; set; }
        public long charid { get; set; }
        public virtual character Character { get; set; }
        public int death { get; set; }
        public int hp { get; set; }
        public int mp { get; set; }
        public byte mjob { get; set; }
        public byte mhflag { get; set; }
        public long nameflags { get; set; }
        public byte sjob { get; set; }
        public int title { get; set; }
        public int twoh { get; set; }

        internal class Map : EntityTypeConfiguration<character_stats>
        {
            public Map()
            {
                ToTable("char_stats");

                HasKey(x => x.charid);

                Property(x => x.bazaar_message).HasMaxLength(120);
                Property(x => x.twoh).HasColumnName("2h");

                HasRequired(x => x.Character).WithRequiredDependent(x => x.character_stats);
            }
        }
    }
}
