using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.Models;

namespace Clinic_Veterinaty_API.Repository.Interfaces
{
    public interface IVetRepository : IBaseRepository
    {
        public Task<Vet> GetByIdVetAsync(int id);
        public Task <IEnumerable<Vet>> GetAllVetsAsync();
        public Task<Vet> DeleteVetByIdAsync(int id);
    }
}