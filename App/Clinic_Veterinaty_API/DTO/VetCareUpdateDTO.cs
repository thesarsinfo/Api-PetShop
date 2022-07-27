using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Veterinaty_API.DTO
{
    public class VetCareUpdateDTO
    {        
        public ulong ClientId { get; set; }
        public int VetId { get; set; }        
        public int DogId { get; set; }
        [Range(0,300,ErrorMessage ="O campo peso do cachorro deve ser entre 0 a 300 kilos")]
        public double Weight { get; set; }   
        
        [MinLength(3,ErrorMessage ="O campo ultimo diagnostico deve conter no mínimo 3 caracteres")]
        public string LastDiagnosis { get; set; }        
        [MinLength(3,ErrorMessage ="O campo comentários deve conter no mínimo 3 caracteres")]
        public string Coments { get; set; }
    }
}