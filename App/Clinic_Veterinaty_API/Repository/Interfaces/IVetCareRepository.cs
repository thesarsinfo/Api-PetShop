using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.Models;

namespace Clinic_Veterinaty_API.Repository.Interfaces
{
    public interface IVetCareRepository : IBaseRepository
    {     
        public Task<Dog> GetDogByClientIdDogId(ulong clientId, int idDog);   
        public Task<Vet> GetByIdVetAsync(int id);
        public Task<IEnumerable<VetCare>> GetAllVetCallsByClient(ulong cpf);
        public Task<IEnumerable<VetCare>> GetAllVetCallsByVet(ulong crmv);
    }
}