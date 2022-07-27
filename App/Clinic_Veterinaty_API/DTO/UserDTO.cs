using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage ="Identification Client is required")]
        public ulong Identification { get; set; }
     
        [Required]
        [EmailAddress]
        public string Email {get;set;}    
  
        [Required]
        [MinLength(6,ErrorMessage ="Customer name needs 3 minimum characters in length")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password),ErrorMessage ="both password need be equals")]
        
        public string ConfirmPassword { get; set; }  
        [Required]             
        public string JobRole { get; set; }   
  
    }
}