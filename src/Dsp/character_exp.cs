using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class character_exp
    {
        public int blm { get; set; }
        public int blu { get; set; }
        public int brd { get; set; }
        public int bst { get; set; }
        public virtual character character { get; set; }
        public long charid { get; set; }
        public int cor { get; set; }
        public int dnc { get; set; }
        public int drg { get; set; }
        public int drk { get; set; }
        public int geo { get; set; }
        public int limits { get; set; }
        public byte merits { get; set; }
        public byte mode { get; set; }
        public int mnk { get; set; }
        public int nin { get; set; }
        public int pld { get; set; }
        public int pup { get; set; }
        public int rdm { get; set; }
        public int rng { get; set; }
        public int run { get; set; }
        public int sam { get; set; }
        public int sch { get; set; }
        public int smn { get; set; }
        public int thf { get; set; }
        public int war { get; set; }
        public int whm { get; set; }

        internal class Map : EntityTypeConfiguration<character_exp>
        {
            public Map()
            {
                ToTable("char_exp");

                HasKey(x => x.charid);

                HasRequired(x => x.character).WithRequiredDependent(x => x.character_exp);
            }
        }
    }
}
