
using Microsoft.AspNetCore.Identity;

namespace westcoast_education2.api.Models;

    public class UserModel: IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
