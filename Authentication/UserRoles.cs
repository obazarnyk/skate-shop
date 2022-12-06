using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace educational_practice5.Authentication
{
    [Flags]
    public enum UserRoles
    {
        Admin,
        User
    }
        
        
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private UserRoles roleEnum;
        public UserRoles RoleEnum
        {
            get { return roleEnum; }
            set { roleEnum = value; base.Roles = value.ToString(); }
        }
    }
}
