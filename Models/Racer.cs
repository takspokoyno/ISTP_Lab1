using System;
using System.Collections.Generic;

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
        public string Name { get; set; } = null!;
        public string Sex { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int TeamId { get; set; }

        public virtual Team Team { get; set; } = null!;
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }
    }
}
