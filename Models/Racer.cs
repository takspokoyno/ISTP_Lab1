﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Labka1.Models
{
    public partial class Racer
    {
        public Racer()
        {
            Cars = new HashSet<Car>();
            Participations = new HashSet<Participation>();
        }

        public int Id { get; set; }
        [Display(Name = "Ім'я")]
        public string Name { get; set; } = null!;
        [Display(Name = "Стать")]
        public string Sex { get; set; } = null!;
        [Display(Name = "Дата народження")]
        public DateTime BirthDate { get; set; }
        public int TeamId { get; set; }
        [Display(Name = "Команда")]
        public virtual Team Team { get; set; } = null!;
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }
    }
}
