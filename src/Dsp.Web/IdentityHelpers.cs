using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Dsp.Web
{
    public static class IdentityHelpers
    {
        public static long GetAccountId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                throw new ArgumentException("Identity must be claims identity.", "identity");
            }

            Claim claim = claimsIdentity.FindFirst(ClaimTypes.Sid);
            if (claim == null) { throw new InvalidOperationException("Claims Identity doesn't have an SID."); }

            return long.Parse(claim.Value);
        }

        public static HashSet<string> GetRoles(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                throw new ArgumentException("Identity must be claims identity.", "identity");
            }

            HashSet<string> roles = new HashSet<string>();

            IEnumerable<string> roleNames = claimsIdentity.FindAll(ClaimTypes.Role).Select(c => c.Value);

            foreach (string roleName in roleNames)
            {
                roles.Add(roleName);
            }

            return roles;
        }
    }
}