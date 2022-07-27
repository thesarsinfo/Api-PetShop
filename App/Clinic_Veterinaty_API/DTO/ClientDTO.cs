using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.DTO
{
    public class ClientDTO
    {
  
        [Required]
        [MinLength(3,ErrorMessage ="Customer name needs 3 minimum characters in length")]
        public string Name { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="Customer name needs 3 minimum characters in length")]
        public string LastName { get; set; }  
        [Required]     
        [MinLength(3,ErrorMessage ="Customer name needs 3 minimum characters in length")]
        public string Address { get; set; }   
        [Required]
        [EmailAddress]
        public string Email {get;set;}                     
        
    }
}