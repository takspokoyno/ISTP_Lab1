﻿using System;
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
        [Display(Name = "Марка")]
        public string Brand { get; set; } = null!;
        [Display(Name = "Модель")]
        public string Model { get; set; } = null!;
        [Display(Name = "Вартість")]
        public decimal Price { get; set; }
        [Display(Name = "Рік випуску")]
        public int Year { get; set; }
        public int OwnerId { get; set; }
        [Display(Name = "Власник")]
        public virtual Racer Owner { get; set; } = null!;
        public virtual ICollection<Part> Parts { get; set; }
    }
}
