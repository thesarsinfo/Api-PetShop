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
    public class ClientRepository : BaseRepository,IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Client> GetByIdClientAsync(ulong id)
        {
            return await _context.Clients.Where(x => x.CPF == id && x.Status == true).FirstOrDefaultAsync();
        }
        public async Task <IEnumerable<Client>> GetAllByIdClientAsync()
        {
            return await _context.Clients.Where(value => value.Status == true).ToListAsync();        
        }

        public async Task<Client> DeleteClientByIdAsync(ulong id)
        {
            return await _context.Clients.FirstOrDefaultAsync(client => client.CPF == id);
        }
    }
}