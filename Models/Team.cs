using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Labka1.Models
{
    public partial class Team
    {
        public Team()
        {
            Racers = new HashSet<Racer>();
            Sponsors = new HashSet<Sponsor>();
        }

        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Racer> Racers { get; set; }
        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
