using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Labka1.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Participations = new HashSet<Participation>();
        }

        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;
        [Display(Name = "Винагорода")]
        public decimal? Reward { get; set; }

        public virtual ICollection<Participation> Participations { get; set; }
    }
}
