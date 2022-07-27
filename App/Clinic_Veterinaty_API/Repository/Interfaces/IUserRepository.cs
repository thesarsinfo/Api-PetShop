using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.Models;

namespace Clinic_Veterinaty_API.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        public Task<User> GetEmailUser(string email);
        public Task<User> GetUserId(ulong id);
    }
}