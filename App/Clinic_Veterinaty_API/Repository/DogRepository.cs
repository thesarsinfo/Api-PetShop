using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.Data;
using Clinic_Veterinaty_API.Models;
using Clinic_Veterinaty_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Veterinaty_API.Repository
{
    public class DogRepository : ClientRepository,IDogRepository
    {
        private readonly ApplicationDbContext _context;

        public DogRepository(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Dog>> GetAllDogsByClientId(ulong clientId)
        {
            return await _context.Dogs.Include(dogs => dogs.Clients).Where(value => value.Status == true && value.Clients.CPF == clientId).ToListAsync();
        }
        public async Task<Dog> GetDogByClientIdDogId(ulong clientId, int idDog)
        {
            return await _context.Dogs.Include(dogs => dogs.Clients).Where(value => value.Id == idDog && value.Status == true && value.Clients.CPF == clientId).FirstOrDefaultAsync();
        }
        public async Task<Dog> GetDogById(int idDog)
        {
            return await _context.Dogs.Include(dogs => dogs.Clients).Where(value => value.Id == idDog && value.Status == true).FirstOrDefaultAsync();
        }
        
        
    }
}