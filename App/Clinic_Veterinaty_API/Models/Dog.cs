using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DogWeight { get; set; }       
        public string DogBreed { get; set; }   
        public double DogHeight {get;set;}    
         
        public DateTime BirthDate { get; set; }     
        public bool Status { get; set; }
        public Client Clients { get; set; }
    }
}