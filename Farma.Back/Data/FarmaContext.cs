using Microsoft.EntityFrameworkCore;
using farma_back.Models;

namespace farma_back.Data;

public class FarmaContext : DbContext
{
    public FarmaContext(DbContextOptions<FarmaContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Farmacia> Farmacias { get; set; }
    public DbSet<Receta> Recetas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Cotizacion> Cotizaciones { get; set; }
    public DbSet<PagoIntent> PagoIntents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configuración Farmacia
        modelBuilder.Entity<Farmacia>(entity =>
        {
            entity.HasIndex(e => new { e.Latitud, e.Longitud });
        });

        // Configuración Pedido
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración Receta
        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasOne(r => r.Pedido)
                .WithMany(p => p.Recetas)
                .HasForeignKey(r => r.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración Cotización
        modelBuilder.Entity<Cotizacion>(entity =>
        {
            entity.HasOne(c => c.Pedido)
                .WithMany(p => p.Cotizaciones)
                .HasForeignKey(c => c.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Farmacia)
                .WithMany(f => f.Cotizaciones)
                .HasForeignKey(c => c.FarmaciaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración PagoIntent
        modelBuilder.Entity<PagoIntent>(entity =>
        {
            entity.HasOne(pi => pi.Pedido)
                .WithMany(p => p.PagoIntents)
                .HasForeignKey(pi => pi.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.IntentId).IsUnique();
        });

        // Seed data para farmacias de prueba
        modelBuilder.Entity<Farmacia>().HasData(
            new Farmacia
            {
                Id = 1,
                Nombre = "Farmacia Central",
                Direccion = "Av. Corrientes 1234",
                Localidad = "Buenos Aires",
                Provincia = "Buenos Aires",
                CodigoPostal = "1043",
                Latitud = -34.6037,
                Longitud = -58.3816,
                Telefono = "011-4567-8901",
                Email = "central@farmacia.com"
            },
            new Farmacia
            {
                Id = 2,
                Nombre = "Farmacia del Barrio",
                Direccion = "Av. Santa Fe 5678",
                Localidad = "Buenos Aires",
                Provincia = "Buenos Aires",
                CodigoPostal = "1425",
                Latitud = -34.5875,
                Longitud = -58.3974,
                Telefono = "011-4567-8902",
                Email = "barrio@farmacia.com"
            }
        );
    }
}
