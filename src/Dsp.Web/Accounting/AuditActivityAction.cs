using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Dsp.Web.Accounting
{
    public class AuditActivityAction : IControllerAction<long>, IDisposable
    {
        private AccountContext _context;
        private bool _dispose;

        public AuditActivityAction()
        {
            _context = new AccountContext();
            _dispose = true;
        }

        public AuditActivityAction(AccountContext context)
        {
            _context = context;
        }

        public long AccountId { get; set; }
        public Dictionary<string, string> Changes { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }

        public void AddChange<T>(string key, T change)
        {
            if (Changes == null) { Changes = new Dictionary<string, string>(); }

            string contents = JsonConvert.SerializeObject(change);

            Changes.Add(key, contents);
        }

        public void Dispose()
        {
            if (_dispose)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        async public Task<long> Execute()
        {
            if (AccountId <= 0) { throw new InvalidOperationException("Invalid Account ID."); }

            AccountActivity activity = new AccountActivity
            {
                Description = Description,
                IPAddress = IPAddress,
                OccurrenceUtc = DateTime.UtcNow
            };

            _context.AccountActivities.Add(activity);

            bool exists = await _context.Accounts.AnyAsync(a => a.AccountId == AccountId);

            if (exists)
            {
                activity.Account_AccountId = AccountId;
            }
            else
            {
                activity.Account = _context.Accounts.Add(new Account
                {
                    AccountId = AccountId,
                    UserName = UserName
                });
            }

            if (Changes != null)
            {
                activity.Changes = Changes.Select(p => _context.AccountActivityChanges.Add(new AccountActivityChange
                {
                    Activity = activity,
                    Contents = p.Value,
                    Key = p.Key
                })).ToList();
            }

            await _context.SaveChangesAsync();

            return activity.AccountActivityId;
        }
    }
}