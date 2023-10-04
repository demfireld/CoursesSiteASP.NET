using Microsoft.AspNetCore.Identity;

namespace courses.ViewModels
{
    public class EditUserRolesViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public EditUserRolesViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
