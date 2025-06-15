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
    public virtual DbSet<RelatorioModel> Relatorios { get; set; }
    public DbSet<AlertaModel> Alertas { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuarioModel>(entity =>
        {
            entity.ToTable("Usuario");
            entity.HasKey(e => e.UsuarioId);

            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.Telefone)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("(00) 00000-0000");

            entity.Property(e => e.Endereco)
                .IsRequired()
                .HasMaxLength(200)
                .HasDefaultValue("Endereço não informado");

            entity.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("00000-000");

            entity.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Cidade não informada");

            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(2)
                .HasDefaultValue("SP");

            entity.Property(e => e.Senha)
                .IsRequired()
                .HasMaxLength(255)
                .HasDefaultValue("senha123");

            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("User");

            entity.Property(e => e.DataCriacao)
                .IsRequired()
                .HasDefaultValueSql("SYSDATE");

            entity.HasMany(u => u.ResiduosEletronicos)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.UsuarioId)
                .IsRequired();
        });

        modelBuilder.Entity<ResiduoEletronicoModel>(entity =>
        {
            entity.ToTable("ResiduosEletronicos");
            entity.HasKey(e => e.ResiduoEletronicoId);

            entity.Property(e => e.Tipo).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Marca).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Modelo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Estado).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<RelatorioModel>(entity =>
        {
            entity.ToTable("Relatorio");
            entity.HasKey(e => e.RelatorioId);

            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.DataGeracao).IsRequired();
            entity.Property(e => e.TipoRelatorio).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Descricao).HasMaxLength(500);
        });
    }
}