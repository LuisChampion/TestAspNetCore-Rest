using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Interfaces;
namespace Entities
{
    public class Survey:ICreate_At
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Activity_Id { get; set; }
    
        [Required]
        [Column(TypeName = "json")]        
        public string Answers { get; set; }

        [ForeignKey(nameof(Activity_Id))]
        public Activity Activity { get; set; }

        [Required]
        public DateTime Created_At { get; set; }
    }
}
