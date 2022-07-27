using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Veterinaty_API.Repository.Interfaces
{
    public interface IBaseRepository
    {
        public void Add<T> (T entity) where T : class;
        public void Update<T> (T entity) where T : class;
        public Task <bool> SaveChangesAsync();
    }
}