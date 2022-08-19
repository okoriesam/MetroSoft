using Metrosoft.Models;
using Microsoft.EntityFrameworkCore;

namespace Metrosoft.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

       public DbSet<Users> Users { get; set; }
    }
}
