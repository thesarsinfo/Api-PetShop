using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.Models;

namespace Clinic_Veterinaty_API.Repository.Interfaces
{
    public interface IClientRepository : IBaseRepository
    {
        public Task<Client> GetByIdClientAsync(ulong id);
        public Task <IEnumerable<Client>> GetAllByIdClientAsync();      
        public Task<Client> DeleteClientByIdAsync(ulong id);
    }
}