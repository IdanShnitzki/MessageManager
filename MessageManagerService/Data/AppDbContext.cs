using MessageManagerService.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageManagerService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Message> Messages { get; set; }

    }
}
