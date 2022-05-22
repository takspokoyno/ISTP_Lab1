using System;
using System.Collections.Generic;

namespace Labka1.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Participations = new HashSet<Participation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Reward { get; set; }

        public virtual ICollection<Participation> Participations { get; set; }
    }
}
