using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace Dsp.Web
{
    public class AuthenticateAction : IControllerAction<long?>
    {
        private readonly DspContext _context;

        public AuthenticateAction(DspContext context)
        {
            _context = context;
        }

        public string EncryptedPassword { get; set; }
        public string UserName { get; set; }

        async public Task<long?> Execute()
        {
            long? id = await _context.accounts.Where(a => a.login == UserName &&
                                                          a.password == EncryptedPassword)
                                              .Select(a => (long?)a.id)
                                              .FirstOrDefaultAsync();

            return id;
        }
    }
}