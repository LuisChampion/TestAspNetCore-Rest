using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Entities.Interfaces;

namespace Entities
{
    public class Activity: ICreateUpdate_At, IStatus
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El {0} es requerido")]
        [Display(Name = "Property")]
        public int Property_Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        public DateTime Schedule { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public DateTime Created_At { get; set; } = DateTime.Now;

        [Required]
        public DateTime Update_At { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(35)]
        public string Status { get; set; }



        [JsonIgnore]
        [ForeignKey(nameof(Property_Id))]
        public virtual Property Property {get;set;}

        public Survey Survey { get; set; }
        
    }
}
