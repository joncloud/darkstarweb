using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Dsp.Web.Accounting
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private DspContext _context;

        public AccountController()
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

        [Route("Password")]
        public Task<HttpResponseMessage> PutPassword(PasswordChangeRequest request)
        {
            var action = new ChangePasswordAction(_context);
            action.AccountId = User.Identity.GetAccountId();
            action.ConfirmPassword = request.ConfirmPassword;
            action.CurrentPassword = request.CurrentPassword;
            action.IPAddress = this.GetIPAddress();
            action.NewPassword = request.NewPassword;
            return action.Execute();
        }
    }
}