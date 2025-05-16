using PruebaTecnica.Entities;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Perfil>(tb => {
                tb.HasKey(col => col.IdPerfil); //declara la llave princippal
                tb.Property(col => col.IdPerfil).UseIdentityColumn().ValueGeneratedOnAdd(); //aumenta uno en uno
                tb.Property(col => col.Nombre).HasMaxLength(50); //longitud de 50 caracteres
                tb.ToTable("Perfil"); //nombre de la tabla en la base de datos
                tb.HasData(
                        new Perfil { IdPerfil = 1, Nombre = "Programador Dev" }, // inserta datos
                        new Perfil { IdPerfil = 2, Nombre = "Programador Senior" }, // inserta datos
                        new Perfil { IdPerfil = 3, Nombre = "Analista" } // inserta datos
                    );
            });

            modelBuilder.Entity<Empleado>(tb => {
                tb.HasKey(col => col.IdEmpleado); //declara la llave princippal
                tb.Property(col => col.IdEmpleado).UseIdentityColumn().ValueGeneratedOnAdd(); // aumenta uno en uno
                tb.Property(col => col.NombreCompleto).HasMaxLength(50); // longitud de 50 caracteres
                tb.HasOne(col => col.PerfilReferencia).WithMany(p => p.EmpleadosReferencia) //relacion uno a muchos
                .HasForeignKey(col => col.IdPerfil); //llave foranea
                tb.ToTable("Empleado"); // nombre de la tabla en la base de datos
            });

        }



    }
}