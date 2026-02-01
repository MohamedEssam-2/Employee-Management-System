using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Demo_DAL.Models.IdentityModels
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
