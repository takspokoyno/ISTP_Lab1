using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Labka1.Models
{
    public partial class Sponsor
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;
        public int TeamId { get; set; }
        [Display(Name = "Команда")]
        public virtual Team? Team { get; set; } = null!;
    }
}
