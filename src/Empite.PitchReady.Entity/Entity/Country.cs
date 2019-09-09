using System;
using System.ComponentModel.DataAnnotations;

namespace Empite.PitchReady.Entity
{
    public class Country
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

    }
}
