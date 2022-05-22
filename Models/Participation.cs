using System;
using System.Collections.Generic;

namespace Labka1.Models
{
    public partial class Participation
    {
        public int Id { get; set; }
        public int RacerId { get; set; }
        public int TournamentId { get; set; }

        public virtual Racer Racer { get; set; } = null!;
        public virtual Tournament Tournament { get; set; } = null!;
    }
}
