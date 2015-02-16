using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class auto_translate_string
    {
        public byte category { get; set; }
        public byte index { get; set; }
        public string value { get; set; }

        internal class Map : EntityTypeConfiguration<auto_translate_string>
        {
            public Map()
            {
                ToTable("auto_translate_strings");

                HasKey(x => new { x.category, x.index });
            }
        }
    }
}
