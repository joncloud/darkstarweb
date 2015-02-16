using Dsp.Web.Accounting;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Dsp.Web
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private static Task AuditLogin(OAuthGrantResourceOwnerCredentialsContext context, AccountContext ctx, long? id)
        {
            AuditActivityAction auditor = new AuditActivityAction(ctx)
            {
                AccountId = id.Value,
                Description = "Login",
                IPAddress = context.Request.RemoteIpAddress,
                UserName = context.UserName
            };

            return auditor.Execute();
        }

        private static Task<IReadOnlyCollection<string>> GetRoleNames(AccountContext ctx, long? id)
        {
            GetRoleNamesAction action = new GetRoleNamesAction(ctx)
            {
                AccountId = id.Value
            };

            return action.Execute();
        }

        private static async Task<long?> GetAccountId(OAuthGrantResourceOwnerCredentialsContext context)
        {
            long? id = null;
            using (DspContext ctx = new DspContext())
            {
                AuthenticateAction authenticator = new AuthenticateAction(ctx)
                {
                    EncryptedPassword = MySqlFunctions.Password(context.Password),
                    UserName = context.UserName
                };

                id = await authenticator.Execute();
            }

            return id;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            long? id = await GetAccountId(context);

            if (id == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            IReadOnlyCollection<string> roleNames;
            using (AccountContext ctx = new AccountContext())
            {
                await AuditLogin(context, ctx, id);

                roleNames = await GetRoleNames(ctx, id);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Sid, id.Value.ToString()));
            
            foreach (string roleName in roleNames)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            }

            context.Validated(identity);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
    }
}