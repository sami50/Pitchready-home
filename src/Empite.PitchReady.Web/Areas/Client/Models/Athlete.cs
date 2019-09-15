using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empite.PitchReady.Web.Areas.Admin;
namespace Empite.PitchReady.Web.Areas.Admin
{
    public class Athlete
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string email { get; set; }

       
        public Client Client { get; set; }
        public string ClientID { get; set; }
    }
}
