using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.DTO
{
    public class DogUpdateDTO
    {       
        
        
        [MinLength(3,ErrorMessage ="O nome do cachorro deve conter 3 caracteres no minimo")]
        public string Name { get; set; }
        [MinLength(2,ErrorMessage ="A raça do cachorro deve conter 2 caracteres no minimo")]
        public string DogBreed { get; set; }
        
        [Range(0,150,ErrorMessage ="O peso do cachorro deve ser pelo menos entre 0 até 150 kilos")]
         public double DogWeight { get; set; }  
        
        [Range(0,300,ErrorMessage ="A altura do cachorro deve ser entre 0 até 300 cm")]
        public double DogHeight {get;set;}   
        [Required(ErrorMessage ="A data de nascimento é requerida")]      
        public DateTime BirthDate { get; set; }        
        public ulong ClientId { get; set; }

    }
}