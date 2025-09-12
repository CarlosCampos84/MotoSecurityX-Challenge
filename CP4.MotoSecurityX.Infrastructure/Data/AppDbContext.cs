using CP4.MotoSecurityX.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CP4.MotoSecurityX.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Moto> Motos => Set<Moto>();
    public DbSet<Patio> Patios => Set<Patio>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // MOTO
        var moto = modelBuilder.Entity<Moto>();
        moto.ToTable("Motos");
        moto.HasKey(x => x.Id);

        // VO Placa como owned
        moto.OwnsOne(x => x.Placa, placa =>
        {
            placa.Property(p => p.Value)   // seu VO usa "Value"
                .HasColumnName("Placa")
                .HasMaxLength(10)
                .IsRequired();

            // índice único na placa (no owned)
            placa.HasIndex(p => p.Value).IsUnique();
        });

        // Relacionamento com Pátio (Moto NÃO tem navegação Patio)
        moto.HasOne<Patio>()
            .WithMany(p => p.Motos)
            .HasForeignKey(x => x.PatioId)
            .OnDelete(DeleteBehavior.SetNull);

        // PÁTIO
        var patio = modelBuilder.Entity<Patio>();
        patio.ToTable("Patios");
        patio.HasKey(x => x.Id);
        patio.Property(x => x.Nome).HasMaxLength(120).IsRequired();
        patio.Property(x => x.Endereco).HasMaxLength(200).IsRequired();
    }
}