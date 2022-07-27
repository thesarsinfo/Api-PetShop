using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Veterinaty_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<VetCare> VetCare { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}