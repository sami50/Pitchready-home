using System;
using System.Collections.Generic;
using System.Text;

namespace Empite.PitchReady.Entity
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
