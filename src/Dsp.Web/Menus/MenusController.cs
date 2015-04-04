using Dsp.Web.Accounting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Dsp.Web.Menus
{
    [Authorize]
    [RoutePrefix("api/Menus")]
    public class MenusController : ApiController
    {
        private static readonly IReadOnlyCollection<Menu> _menus;

        static MenusController()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu { Href = "#/Dashboard", Text = "Dashboard" },
                new Menu { Href = "#/Activities", Text = "Activities", RequiredRoleName = Role.Administrator },
                new Menu { Href = "#/AuctionHouse", Text = "Auction House" }
            };

            _menus = new ReadOnlyCollection<Menu>(menus);
        }

        [Route]
        public IEnumerable<Menu> Get()
        {
            HashSet<string> roles = User.Identity.GetRoles();

            IEnumerable<Menu> menus = _menus.Where(m => m.RequiredRoleName == null || roles.Contains(m.RequiredRoleName));

            return menus;
        }
    }
}