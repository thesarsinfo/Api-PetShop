using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.DTO
{
    public class VetUpdateDTO
    {
        
        [MinLength(3,ErrorMessage ="Vet name needs 3 minimum characters in length")]
        public string Name { get; set; }
        [MinLength(3,ErrorMessage ="Vet lastname needs 3 minimum characters in length")]
        public string LastName { get; set; }            
        [MinLength(3,ErrorMessage ="Vet Address needs 3 minimum characters in length")]
        public string Address { get; set; }          
        [EmailAddress]
        public string Email {get;set;}       
       
    }
}