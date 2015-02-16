using Dsp.Web.Accounting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp.Web
{
    public class GetRoleNamesAction : IControllerAction<IReadOnlyCollection<string>>
    {
        private readonly AccountContext _context;

        public GetRoleNamesAction(AccountContext context)
        {
            _context = context;
        }

        public long AccountId { get; set; }

        async public Task<IReadOnlyCollection<string>> Execute()
        {
            string[] roleNames = await _context.AccountRoles.Where(a => a.Account_AccountId == AccountId)
                .Select(a => a.Role.Name)
                .ToArrayAsync();

            return new ReadOnlyCollection<string>(roleNames);
        }
    }
}
