using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.Data;
using Clinic_Veterinaty_API.Repository.Interfaces;

namespace Clinic_Veterinaty_API.Repository
{
    public class BaseRepository :IBaseRepository
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public async Task <bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }    
    }
}