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
    public class UserRepository : BaseRepository,IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User> GetUserId(ulong id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Identification == id);
        }
        public async Task<User> GetEmailUser(string email)
        {
            return await _context.Users.Where(em => em.Email == email).FirstOrDefaultAsync();
        }

    }
}