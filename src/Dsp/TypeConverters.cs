using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public static class TypeConverters
    {
        public static DateTime? FromRaw(long raw)
        {
            if (raw <= 0)
            {
                return null;
            }

            return new DateTime(1970, 1, 1).AddSeconds(raw);
        }

        public static long ToRaw(DateTime? date)
        {
            if (date.HasValue)
            {
                return (long)(DateTime.UtcNow - date.Value).TotalSeconds;
            }

            return 0;
        }
    }
}
