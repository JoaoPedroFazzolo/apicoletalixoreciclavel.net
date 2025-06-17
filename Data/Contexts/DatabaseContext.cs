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
    public virtual DbSet<RelatorioModel> Relatorios { get; set; }
    public DbSet<AlertaModel> Alertas { get; set; }
    public DbSet<NotificacaoModel> Notificacoes { get; set; }
    public DbSet<DestinacaoModel> Destinacoes { get; set; }

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
                .HasMaxLength(20);

            entity.Property(e => e.Endereco)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(2);

            entity.Property(e => e.Senha)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);

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

        modelBuilder.Entity<ColetaModel>(entity =>
        {
            entity.ToTable("Coleta");
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
            entity.ToTable("Ponto_coleta");
            entity.HasKey(e => e.PontoColetaId);

            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Endereco).IsRequired();
            entity.Property(e => e.Capacidade).IsRequired();

            entity.HasMany(p => p.Coletas)
                .WithOne(c => c.PontoColeta)
                .HasForeignKey(c => c.PontoColetaId);
        });

        modelBuilder.Entity<NotificacaoModel>(entity =>
        {
            entity.ToTable("Notificacao");
            entity.HasKey(e => e.NotificacaoId);

            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Mensagem).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Tipo).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50).HasDefaultValue("NaoLida");
            entity.Property(e => e.DataCriacao).IsRequired().HasDefaultValueSql("SYSDATE");

            entity.HasOne(n => n.Usuario)
                .WithMany()
                .HasForeignKey(n => n.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<DestinacaoModel>(entity =>
        {
            entity.ToTable("Destinacao");
            entity.HasKey(e => e.DestinacaoId);

            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Tipo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).HasMaxLength(1000);
            entity.Property(e => e.Endereco).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Telefone).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.ResponsavelTecnico).HasMaxLength(200);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CapacidadeMaxima).HasColumnType("decimal(18,2)");
            entity.Property(e => e.UnidadeCapacidade).HasMaxLength(50);
            entity.Property(e => e.DataCadastro).IsRequired();
            entity.Property(e => e.DataAtualizacao).IsRequired(false);
            entity.Property(e => e.Observacoes).HasMaxLength(500);
            entity.Property(e => e.DiasAtendimento).HasMaxLength(200);

            entity.Property(e => e.PermiteColeta)
                .IsRequired()
                .HasColumnType("NUMBER(1)")
                .HasConversion<int>();

            entity.Property(e => e.HorarioFuncionamentoInicio)
                .HasMaxLength(8)
                .HasColumnType("VARCHAR2(8)");

            entity.Property(e => e.HorarioFuncionamentoFim)
                .HasMaxLength(8)
                .HasColumnType("VARCHAR2(8)");

            entity.HasIndex(e => e.Nome).IsUnique();
            entity.HasIndex(e => e.Tipo);
            entity.HasIndex(e => e.Status);
        });
    }
}