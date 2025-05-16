using PruebaTecnica.Entities;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Machine> Machine { get; set; }
        public DbSet<Component> Component { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Machine>(tb =>
            {
                tb.HasKey(m => m.Id);
                tb.Property(m => m.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(m => m.TechnicalLocation).HasMaxLength(100);
                tb.Property(m => m.Description).HasMaxLength(200);
                tb.Property(m => m.Model).HasMaxLength(50);
                tb.Property(m => m.SerialNumber).HasMaxLength(50);
                tb.Property(m => m.MachineTypeName).HasMaxLength(50);
                tb.Property(m => m.BrandName).HasMaxLength(50);
                tb.Property(m => m.Criticality).HasMaxLength(20);
                tb.Property(m => m.Sector).HasMaxLength(50);
                tb.ToTable("Machine");
            });


            modelBuilder.Entity<Component>(tb =>
            {
                tb.HasKey(c => c.Id);
                tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(c => c.Part).HasMaxLength(100);
                tb.Property(c => c.ComponentType).HasMaxLength(50);
                tb.Property(c => c.BrandName).HasMaxLength(50);
                tb.Property(c => c.Model).HasMaxLength(50);
                tb.Property(c => c.Description).HasMaxLength(200);
                tb.Property(c => c.SerialNumber).HasMaxLength(50);
                tb.HasOne(c => c.Machine)
                  .WithMany(m => m.Components)
                  .HasForeignKey(c => c.MachineId);
                tb.ToTable("Component");
            });

        }



    }
}