using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.Models
{
    public class VetCare
    {
        public int Id { get; set; }
        public Client Clients { get; set; }
        public Vet Vets { get; set; }
        public Dog Dogs { get; set; }
        public DateTime Hour { get; set; }
        public double Weight { get; set; }
        public double Age { get; set; }        
        public string LastDiagnosis { get; set; }
        public string Coments { get; set; }
    }
}