using System.ComponentModel.DataAnnotations;

namespace Clinic_Veterinaty_API.Models
{
    public class User
    {
        [Key]
        public ulong Identification { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }        
        public string JobRole { get; set; }
    }
}