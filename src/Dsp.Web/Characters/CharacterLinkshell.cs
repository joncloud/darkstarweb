using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Characters
{
    public class CharacterLinkshell
    {
        public string Name { get; set; }
        public LinkshellStatus Status { get; set; }
        public string StatusText { get { return Status.ToString(); } }
        public bool UserCanView { get; set; }
    }
}