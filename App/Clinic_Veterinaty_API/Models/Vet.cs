using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.Models
{
    public class Vet
    {
        [Key]
        public int CRMV { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }       
        public string Address { get; set; }   
        public string Email {get;set;}             
        public bool Status { get; set; }
    }
}