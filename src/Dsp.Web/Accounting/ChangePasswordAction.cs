using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Dsp.Web.Accounting
{
    public class ChangePasswordAction : IControllerAction
    {
        private readonly DspContext _context;

        public ChangePasswordAction(DspContext context)
        {
            _context = context;
        }

        public long AccountId { get; set; }
        public string ConfirmPassword { get; set; }
        public string CurrentPassword { get; set; }
        public string IPAddress { get; set; }
        public string NewPassword { get; set; }

        async public Task<HttpResponseMessage> Execute()
        {
            account account = await _context.accounts.Where(a => a.id == AccountId).SingleOrDefaultAsync();

            if (account == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (NewPassword != ConfirmPassword)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (account.password != MySqlFunctions.Password(CurrentPassword))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            // Audit the password change.
            using (AuditActivityAction auditor = new AuditActivityAction())
            {
                auditor.AccountId = AccountId;
                auditor.Description = "Change Password";
                auditor.IPAddress = IPAddress;

                // Change the passowrd.
                account.password = MySqlFunctions.Password(NewPassword);
                await _context.SaveChangesAsync();

                await auditor.Execute();
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}