using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dsp.Web.Chat
{
    [Authorize]
    [RoutePrefix("api/Chat")]
    public class ChatController : ApiController
    {
        private DspContext _context;

        public ChatController()
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

        [Route("Linkshells/{name}")]
        public Task<HttpResponseMessage> GetLinkshell(string name, [FromUri]PageSettings pageSettings)
        {
            var action = new GetLinkshellChatMessages(_context);
            action.LinkshellName = name;
            action.OwnerAccountId = User.Identity.GetAccountId();
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }

        [Route("Tells/{id}")]
        public Task<HttpResponseMessage> GetTells(long id, [FromUri]PageSettings pageSettings)
        {
            var action = new GetTellChatMessages(_context);
            action.CharacterId = id;
            action.OwnerAccountId = User.Identity.GetAccountId();
            action.PageSettings = pageSettings;
            action.RequestUri = Request.RequestUri;
            return action.Execute();
        }
    }
}
