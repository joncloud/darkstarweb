using Dsp.Web.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dsp.Web.Auctions
{
    [Authorize]
    [RoutePrefix("api/AuctionHouseItems")]
    public class AuctionHouseItemsController : ApiController
    {
        private DspContext _context;

        public AuctionHouseItemsController()
        {
            _context = new DspContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        [Route]
        public Task<HttpResponseMessage> Get([FromUri]PageSettings pageSettings)
        {
            var action = new GetAllAuctionHouseItemsAction(_context);
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }

        [Authorize]
        [Route("My")]
        public Task<HttpResponseMessage> GetMyItems([FromUri] PageSettings pageSettings)
        {
            var action = new GetMyAuctionHouseItemsActon(_context);
            action.OwnerAccountId = User.Identity.GetAccountId();
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }
    }
}
