using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empite.PitchReady.Web.Areas.Admin
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string UserGuid { get; set; }
    }
}
