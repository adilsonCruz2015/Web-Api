using Microsoft.Owin.Security.OAuth;
using OAuthServer.Data.Repositories;
using OAuthServer.Domain.Contracts;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OAuthServer.Api.Security
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using(IUserRepository _repository = new UserRepository(new Data.DataContexts.OAuthServerDataContext()))
            {
                var user = _repository.Autenticate(context.UserName, context.Password);

                if(object.Equals(user, null))
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("username", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            context.Validated(identity);
        }


    }
}