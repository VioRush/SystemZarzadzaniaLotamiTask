using Microsoft.AspNetCore.Identity;
using SystemZarzadzaniaLotami.Enums;

namespace SystemZarzadzaniaLotami.Models
{
    public class User : IdentityUser
    {
        public Role Role { get; set; }
    }
}
