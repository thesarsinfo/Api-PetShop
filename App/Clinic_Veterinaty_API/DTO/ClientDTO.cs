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
        [MinLength(3,ErrorMessage ="O nome do cliente deve conter no mínimo 3 caracteres")]
        public string Name { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="O sobrenome do cliente deve conter no mínimo 3 caracteres")]
        public string LastName { get; set; }  
        [Required(ErrorMessage = "O endereço do cliente é requerido")]     
        [MinLength(3,ErrorMessage ="O endereço do cliente deve conter no mínimo 3 caracteres")]
        public string Address { get; set; }                     
        
    }
}