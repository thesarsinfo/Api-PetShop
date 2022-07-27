using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Veterinaty_API.DTO
{
    public class VetCareDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage ="CPF Client is required")]
        [Range(10000000000,99999999999,ErrorMessage = "Invalid CPF,The cpf number must contain only 11 numbers")]      
        public ulong ClientId { get; set; }
        [Required (ErrorMessage = "Vet CRMV Field is required")]
        [Range(1,int.MaxValue,ErrorMessage = "The vet id must be 1 to 21000000000")]

        public int VetId { get; set; }
        [Required (ErrorMessage = "Dog Id Field is required")]
        public int DogId { get; set; }
        [Required (ErrorMessage = "Dog Weight Field is required")]
        [Range(0,300,ErrorMessage ="The dog's weight must be between 0 and 300 kilos")]
        public double Weight { get; set; }
   
        [Required(ErrorMessage = "LastDiagnosis field is required")]
        [MinLength(3,ErrorMessage ="Last diagnosis  needs 3 minimum characters length")]
        public string LastDiagnosis { get; set; }
        [Required(ErrorMessage = "LastDiagnosis field is required")]
        [MinLength(3,ErrorMessage ="Last diagnosis  needs 3 minimum characters length")]
        public string Coments { get; set; }
    }
}