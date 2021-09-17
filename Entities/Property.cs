using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public DateTime Created_At { get; set; }

        [Required]
        public DateTime Update_At { get; set; }
        
        public DateTime? Disabled_At { get; set; }

        [Required]
        [MaxLength(35)]
        public string Status { get; set; }
    }
}
