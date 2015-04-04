using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dsp.Web.Accounting
{
    [Authorize(Roles = Role.Administrator)]
    [RoutePrefix("api/Activities")]
    public class ActivitiesController : ApiController
    {
        private AccountContext _context;

        public ActivitiesController()
        {
            _context = new AccountContext();
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
            var action = new GetAllAccountActivities(_context);
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }
    }
}
