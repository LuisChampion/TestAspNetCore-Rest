using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Interfaces;

namespace Entities
{
    public class Activity: ICreate_Update_At, IStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Property_Id { get; set; }

        [Required]
        public DateTime Schedule { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public DateTime Created_At { get; set; }

        [Required]
        public DateTime Update_At { get; set; }

        [Required]
        [MaxLength(35)]
        public string Status { get; set; }

        [ForeignKey(nameof(Property_Id))]
        public virtual Property Property {get;set;}

        public Survey Survey { get; set; }
        
    }
}
