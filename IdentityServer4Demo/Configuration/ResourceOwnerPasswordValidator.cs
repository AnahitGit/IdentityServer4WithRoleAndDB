using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4Demo.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IUserRepository _userRepository;
        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _userRepository.Login(context.UserName, context.Password).Result;
            if (user != null)
            {
                context.Result = new GrantValidationResult(
                       user.Id.ToString(),
                       "custom",
                       new List<Claim>
                       {
                       new Claim(JwtClaimTypes.Name, user.UserName),
                       new Claim(JwtClaimTypes.Email, "gmanahit@gmail.com"),
                       new Claim(JwtClaimTypes.Role, user.Role),
                       new Claim("customclaim","customclaim")
                       });
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
            return Task.CompletedTask;
        }
    }
}
