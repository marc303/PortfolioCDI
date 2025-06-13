using BiblioPortCartier.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BiblioPortCartier.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Document>()
                .HasOne(a => a.Loan)
                .WithOne(a => a.Document)
                .HasForeignKey<Loan>(c => c.DocumentCode);

            builder.Entity<Document>()
                .HasOne(a => a.Reservation)
                .WithOne(a => a.Document)
                .HasForeignKey<Reservation>(c => c.DocumentCode);

            base.OnModelCreating(builder);
        }
    }
}
