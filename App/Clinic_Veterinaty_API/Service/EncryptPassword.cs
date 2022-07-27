using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.Service
{
    public class EncryptPassword
    {
        public string EncryptPasswordMethod(string passwordUser)
        {
            var password = System.Text.ASCIIEncoding.ASCII.GetBytes(passwordUser);
            var encrypted = Convert.ToBase64String(password);
            return encrypted;
        }
    }
}