using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.Models;

namespace Clinic_Veterinaty_API.Repository.Interfaces
{
    public interface IDogRepository : IBaseRepository,IClientRepository
    {
        public Task<IEnumerable<Dog>> GetAllDogsByClientId(ulong clientId);
        public Task<Dog> GetDogByClientIdDogId(ulong clientId, int idDog);
        public Task<Dog> GetDogById(int idDog);
    }
}