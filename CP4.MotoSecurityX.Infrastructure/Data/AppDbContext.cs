using CP4.MotoSecurityX.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CP4.MotoSecurityX.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Moto> Motos => Set<Moto>();
    public DbSet<Patio> Patios => Set<Patio>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ===== MOTO =====
        var moto = modelBuilder.Entity<Moto>();
        moto.ToTable("Motos");
        moto.HasKey(x => x.Id);

        moto.Property(x => x.Modelo).HasMaxLength(120).IsRequired();
        moto.Property(x => x.DentroDoPatio).IsRequired();

        // VO Placa como owned type
        moto.OwnsOne(x => x.Placa, placa =>
        {
            placa.Property(p => p.Value)
                 .HasColumnName("Placa")
                 .HasMaxLength(10)
                 .IsRequired();

            // Índice único na coluna "Placa" (owned)
            placa.HasIndex(p => p.Value).IsUnique();
        });

        // Relacionamento com Pátio (Moto não tem navegação Patio)
        moto.HasOne<Patio>()
            .WithMany(p => p.Motos)
            .HasForeignKey(x => x.PatioId)
            .OnDelete(DeleteBehavior.SetNull);

        // ===== PÁTIO =====
        var patio = modelBuilder.Entity<Patio>();
        patio.ToTable("Patios");
        patio.HasKey(x => x.Id);
        patio.Property(x => x.Nome).HasMaxLength(120).IsRequired();
        patio.Property(x => x.Endereco).HasMaxLength(200).IsRequired();

        // ===== USUÁRIO =====
        var usuario = modelBuilder.Entity<Usuario>();
        usuario.ToTable("Usuarios");
        usuario.HasKey(x => x.Id);
        usuario.Property(x => x.Nome).HasMaxLength(120).IsRequired();
        usuario.Property(x => x.Email).HasMaxLength(200).IsRequired();
        usuario.HasIndex(x => x.Email).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
