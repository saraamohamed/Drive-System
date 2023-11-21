using Microsoft.EntityFrameworkCore;

namespace MVC_Task_Codid.Models
{
    public class DriveDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }

        public DriveDbContext() : base() { 
        
        }
        public DriveDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DriveSystemMvcDB;Integrated Security=True;TrustServerCertificate=True");
            }
        }
    }
}
