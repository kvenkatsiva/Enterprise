using Enterprise.Models;
using Microsoft.EntityFrameworkCore;


namespace Enterprise.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contacts> Contact { get; set; }
    }
}
