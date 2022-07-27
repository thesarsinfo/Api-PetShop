using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email {get;set;}      
        [Required]        
        public string Password { get; set; }

        
     
  
    }
}