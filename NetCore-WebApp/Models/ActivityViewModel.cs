using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_WebApp.Models
{
    public class ActivityViewModel
    {       
            public int Id { get; set; }
          
            [Display(Name = "Property")]
            public int Property_Id { get; set; }
      
            public DateTime Schedule { get; set; }
            public string Title { get; set; }   
            public DateTime Created_At { get; set; } = DateTime.Now;

            [Required(ErrorMessage = "El {0} es requerido")]
            [MaxLength(35)]
            public string Status { get; set; }           

    }
}
