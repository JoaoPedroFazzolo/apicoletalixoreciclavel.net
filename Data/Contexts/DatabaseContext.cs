using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Data.Contexts;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UsuarioModel> Usuarios { get; set; }
    public virtual DbSet<ResiduoEletronicoModel> ResiduoEletronicos { get; set; }
    public virtual DbSet<ColetaModel> Coletas { get; set; }
    public virtual DbSet<PontoColetaModel> PontoColetas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuarioModel>(entity =>
        {
            entity.ToTable("usuario");
            entity.HasKey(e => e.UsuarioId);
            
            entity.Property(e => e.Nome).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Senha).IsRequired();
            entity.Property(e => e.Role).IsRequired();
            
            entity.HasMany(u => u.ListaResiduos)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.UsuarioId)
                .IsRequired();
        });

        modelBuilder.Entity<ResiduoEletronicoModel>(entity =>
        {
            entity.ToTable("ResiduosEletronicos");
            entity.HasKey(e => e.ResiduoEletronicoId);

            entity.Property(e => e.Tipo).IsRequired();
            entity.Property(e => e.Marca).IsRequired();
            entity.Property(e => e.Modelo).IsRequired();
            entity.Property(e => e.Estado).IsRequired();
            entity.Property(e => e.Status).IsRequired();
        });

        modelBuilder.Entity<ColetaModel>(entity =>
        {
            entity.ToTable("coleta");
            entity.HasKey(e => e.ColetaId);

            entity.Property(e => e.DataColeta).IsRequired();

            entity.HasOne(c => c.PontoColeta)
                .WithMany(p => p.Coletas)
                .HasForeignKey(c => c.PontoColetaId)
                .IsRequired();

            entity.HasOne(c => c.Residuo)
                .WithMany()
                .HasForeignKey(c => c.ResiduoId)
                .IsRequired();
        });

        modelBuilder.Entity<PontoColetaModel>(entity =>
        {
            entity.ToTable("ponto_coleta");
            entity.HasKey(e => e.PontoColetaId);

            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Endereco).IsRequired();
            entity.Property(e => e.Capacidade).IsRequired();

            entity.HasMany(p => p.Coletas)
                .WithOne(c => c.PontoColeta)
                .HasForeignKey(c => c.PontoColetaId);
        });
    }
}