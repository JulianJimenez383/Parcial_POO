using ParcialPOO.Modelos;
using ParcialPOO.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace ParcialPOO.DataAccess
{
    public class EmpleadoDbContext : DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("empleados.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(col => col.IdEmpleado);
                entity.Property(col => col.IdEmpleado).IsRequired().ValueGeneratedOnAdd();
            }
            );
        }
        public class AppDbContext : DbContext
        {
            public DbSet<Empleado> Empleados { get; set; }

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }
        }


    }
}
