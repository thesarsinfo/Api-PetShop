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
    public class VetRepository : BaseRepository,IVetRepository
    {
        private readonly ApplicationDbContext _context;

        public VetRepository(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }
        public async Task<Vet> GetByIdVetAsync(int id)
        {
            return await _context.Vets.Where(vet => vet.CRMV == id).FirstOrDefaultAsync();
        }
        public async Task <IEnumerable<Vet>> GetAllVetsAsync()
        {
            return await _context.Vets.Where(vet => vet.Status == true).ToListAsync();        
        }

        public async Task<Vet> DeleteVetByIdAsync(int id)
        {
            return await _context.Vets.FirstOrDefaultAsync(vet => vet.CRMV == id);
        }
    }
}