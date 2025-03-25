using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Models
{
    public class ManageUsersViewModel
    {
        //管理员
        public IdentityUser[] Administrators { get;set; }

        //用户
        public IdentityUser[] Everyone { get; set; }
    }
}
