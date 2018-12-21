using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Models.Entities;

namespace Services.Implements
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
    }
}