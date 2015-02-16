using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Menus
{
    public class Menu
    {
        public Menu[] Children { get; set; }
        public string Href { get; set; }

        [JsonIgnore]
        public string RequiredRoleName { get; set; }

        public string Text { get; set; }
    }
}