using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Data.Contexts;

public class DatabaseContext : DbContext
{
    public virtual DbSet<UsuarioModel> Usuarios { get; set; }
    public virtual DbSet<ResiduoEletronicoModel> ResiduoEletronicos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<UsuarioModel>(entity =>
        // {
        //     entity.Totable("usuario");
        // });

        modelBuilder.Entity<ResiduoEletronicoModel>(entity =>
        {
            entity.ToTable("ResiduosEletronicos");
            entity.HasKey(e => e.ResiduoEletronicoId);

            entity.Property(e => e.Tipo).IsRequired();
            entity.Property(e => e.Marca).IsRequired();
            entity.Property(e => e.Modelo).IsRequired();
            entity.Property(e => e.Estado).IsRequired();
            entity.Property(e => e.Status).IsRequired();

            entity.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .IsRequired();
        });

    }
}