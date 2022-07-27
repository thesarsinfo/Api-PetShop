using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.DTO
{
    public class DogUpdateDTO
    {       
        
        
        [MinLength(3,ErrorMessage ="Dog name needs 3 minimum characters in length")]

        public string Name { get; set; }
        
        public string DogBreed { get; set; }
        
        [Range(0,300,ErrorMessage ="The dog's weight must be between 0 and 300 kilos")]
         public double DogWeight { get; set; }  
        
        [Range(0,3000,ErrorMessage ="The dog's weight must be between 0 and 3000 Height cm")]
        public double DogHeight {get;set;}         
        public DateTime BirthDate { get; set; }        
        public ulong ClientId { get; set; }

    }
}