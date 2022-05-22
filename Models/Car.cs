using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Labka1.Models
{
    public partial class Car
    {
        public Car()
        {
            Parts = new HashSet<Part>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Field cannot be empty")]
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
        public int Year { get; set; }
        public int OwnerId { get; set; }

        public virtual Racer Owner { get; set; } = null!;
        public virtual ICollection<Part> Parts { get; set; }
    }
}
