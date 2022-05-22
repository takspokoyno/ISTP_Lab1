using System;
using System.Collections.Generic;

namespace Labka1.Models
{
    public partial class Part
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null!;
        public int CarId { get; set; }

        public virtual Car? Car { get; set; } = null!;
    }
}
