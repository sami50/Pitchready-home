using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Empite.PitchReady.Entity
{
    public class Client
    {
        [Key]
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public List<Athlete> Athlete { get; set; }
    }
}
