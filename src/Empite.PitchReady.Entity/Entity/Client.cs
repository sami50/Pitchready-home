using System;
using System.ComponentModel.DataAnnotations;

namespace Empite.PitchReady.Entity
{
    public class Client
    {
        [Key]
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

    }
}
