using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Services
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
            
        }
        public DbSet<Contact>Contacts { get; set; }
    }
}
