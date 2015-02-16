using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class auto_translate_item
    {
        public long id { get; set; }
        public string @string { get; set; }

        internal class Map : EntityTypeConfiguration<auto_translate_item>
        {
            public Map()
            {
                ToTable("auto_translate_items");

                HasKey(x => x.id);
            }
        }
    }
}
