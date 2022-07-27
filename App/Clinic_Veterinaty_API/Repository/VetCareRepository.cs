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
    public class VetCareRepository : BaseRepository ,IVetCareRepository
    {
        private readonly ApplicationDbContext _context;


        public VetCareRepository(ApplicationDbContext context) : base (context)
        {
            _context = context;          
        }
        public async Task<IEnumerable<VetCare>> GetAllVetCallsByClient(ulong cpf)
        {
            return await _context.VetCare.Include(cli => cli.Clients)
                                        .Include(ve => ve.Vets)
                                        .Include(dog => dog.Dogs)
                                        .Where(clie => clie.Clients.CPF == cpf)
                                        .ToListAsync();
        }
        public async Task<IEnumerable<VetCare>> GetAllVetCallsByVet(ulong crmv)
        {
            return await _context.VetCare.Include(cli => cli.Clients)
                                        .Include(ve => ve.Vets)
                                        .Include(dog => dog.Dogs)
                                        .Where(vet => vet.Vets.CRMV == (int)crmv)
                                        .ToListAsync();
        }
        public async Task<Dog> GetDogByClientIdDogId(ulong clientId, int idDog)
        {
            return await _context.Dogs.Include(dogs => dogs.Clients)
                        .Where(value => value.Id == idDog && value.Status == true && value.Clients.CPF == clientId)
                        .FirstOrDefaultAsync();
        }
        public async Task<Vet> GetByIdVetAsync(int id)
        {
            return await _context.Vets.Where(vet => vet.CRMV == id).FirstOrDefaultAsync();
        }
        public async Task<VetCare> GetVetCallById(int id)
        {
            return await _context.VetCare
                            .Include(cli => cli.Clients)
                            .Include(vet => vet.Vets)
                            .Include(dog => dog.Dogs)   
                            .Where(call => call.Id == id)
                            .FirstOrDefaultAsync();
        }
        public async Task<Client> GetClientById(ulong id)
        {
            return await _context.Clients.Where(cli => cli.CPF == id).FirstOrDefaultAsync();
        }
    
    }
}