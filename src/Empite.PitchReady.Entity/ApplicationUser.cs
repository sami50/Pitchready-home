using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Empite.PitchReady.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; }
    }
}
