using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities.Interfaces;

namespace Entities
{
    public class Property: ICreateUpdate_At, IStatus
    {
        public Property()
        {
            this.Activities = new HashSet<Activity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Created_At { get; set; }

        [Required]
        public DateTime Update_At { get; set; }
        
        public DateTime? Disabled_At { get; set; }

        [Required]
        [MaxLength(35)]
        public string Status { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
