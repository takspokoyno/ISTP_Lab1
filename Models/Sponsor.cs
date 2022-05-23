using System;
using System.Collections.Generic;

namespace Labka1.Models
{
    public partial class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TeamId { get; set; }

        public virtual Team? Team { get; set; } = null!;
    }
}
