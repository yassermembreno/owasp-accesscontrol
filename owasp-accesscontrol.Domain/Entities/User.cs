using System;
using System.Collections.Generic;

namespace owasp_accesscontrol.Domain.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
    }
}
