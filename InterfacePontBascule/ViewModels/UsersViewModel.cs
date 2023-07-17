using Microsoft.AspNetCore.Identity;

namespace InterfacePontBascule.ViewModels
{
    public class UsersViewModel
    {
        public IdentityUser IdentityUser { get; set; }

        public IList<string> IdentityRoles { get; set; }

        public bool IsEnabled { get; set; }
    }
}
