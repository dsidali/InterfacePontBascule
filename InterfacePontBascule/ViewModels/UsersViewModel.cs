using Microsoft.AspNetCore.Identity;

namespace InterfacePontBascule.ViewModels
{
    public class UsersViewModel
    {
        public IdentityUser IdentityUser { get; set; }

        public List<IdentityRole> IdentityRoles { get; set; }

    }
}
