using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.DTO
{
    public class DogDTO
    {
        [Required (ErrorMessage = "Dog Id Field is required")]
        public int Id { get; set; }
        [Required (ErrorMessage = "Dog name Field is required")]
        [MinLength(3,ErrorMessage ="Dog name needs 3 minimum characters in length")]

        public string Name { get; set; }
        [Required (ErrorMessage = "Dog breed field is required")]
        public string DogBreed { get; set; }

        [Required (ErrorMessage = "Dog Weight Field is required")]
        [Range(0,300,ErrorMessage ="The dog's weight must be between 0 and 300 kilos")]
         public double DogWeight { get; set; }  

        [Required (ErrorMessage = "Dog Height Field is required")]
        [Range(0,3000,ErrorMessage ="The dog's weight must be between 0 and 3000 Height cm")]
        public double DogHeight {get;set;}          

        [Required (ErrorMessage = "Birth age field is required")]       
        public DateTime BirthDate { get; set; }

        [Required (ErrorMessage = "Client Id field is required")]    
        public ulong ClientId { get; set; }

    }
}