﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using apicoletalixoreciclavel.Data.Contexts;

#nullable disable

namespace apicoletalixoreciclavel.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250616100336_UpdateTableNamesAndAddEmpresaDestinacao")]
    partial class UpdateTableNamesAndAddEmpresaDestinacao
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("apicoletalixoreciclavel.Models.AlertaModel", b =>
                {
                    b.Property<long>("AlertaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AlertaId"));

                    b.Property<DateTime>("DataAlerta")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("TipoAlerta")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<long?>("UsuarioId")
                        .HasColumnType("NUMBER(19)");

                    b.HasKey("AlertaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Alertas");
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.ColetaModel", b =>
                {
                    b.Property<long>("ColetaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ColetaId"));

                    b.Property<DateTime>("DataColeta")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<long>("PontoColetaId")
                        .HasColumnType("NUMBER(19)");

                    b.Property<long>("ResiduoId")
                        .HasColumnType("NUMBER(19)");

                    b.HasKey("ColetaId");

                    b.HasIndex("PontoColetaId");

                    b.HasIndex("ResiduoId");

                    b.ToTable("Coleta", (string)null);
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.DestinacaoModel", b =>
                {
                    b.Property<long>("DestinacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DestinacaoId"));

                    b.Property<decimal?>("CapacidadeMaxima")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("NVARCHAR2(1000)");

                    b.Property<string>("DiasAtendimento")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<string>("HorarioFuncionamentoFim")
                        .HasMaxLength(8)
                        .HasColumnType("VARCHAR2(8)");

                    b.Property<string>("HorarioFuncionamentoInicio")
                        .HasMaxLength(8)
                        .HasColumnType("VARCHAR2(8)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<int>("PermiteColeta")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("ResponsavelTecnico")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("UnidadeCapacidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.HasKey("DestinacaoId");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.HasIndex("Status");

                    b.HasIndex("Tipo");

                    b.ToTable("Destinacao", (string)null);
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.EmpresaDestinacaoModel", b =>
                {
                    b.Property<long>("EmpresaDestinacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EmpresaDestinacaoId"));

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(7)")
                        .HasDefaultValueSql("SYSDATE");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.HasKey("EmpresaDestinacaoId");

                    b.ToTable("Empresa_destinacao", (string)null);
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.NotificacaoModel", b =>
                {
                    b.Property<long>("NotificacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("NotificacaoId"));

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(7)")
                        .HasDefaultValueSql("SYSDATE");

                    b.Property<DateTime?>("DataLeitura")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("NVARCHAR2(1000)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasDefaultValue("NaoLida");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.Property<long?>("UsuarioId")
                        .HasColumnType("NUMBER(19)");

                    b.HasKey("NotificacaoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Notificacao", (string)null);
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.PontoColetaModel", b =>
                {
                    b.Property<long>("PontoColetaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PontoColetaId"));

                    b.Property<long>("Capacidade")
                        .HasColumnType("NUMBER(19)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("PontoColetaId");

                    b.ToTable("Ponto_coleta", (string)null);
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.RelatorioModel", b =>
                {
                    b.Property<long>("RelatorioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RelatorioId"));

                    b.Property<DateTime>("DataGeracao")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)");

                    b.Property<string>("TipoRelatorio")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.HasKey("RelatorioId");

                    b.ToTable("Relatorio", (string)null);
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.ResiduoEletronicoModel", b =>
                {
                    b.Property<long>("ResiduoEletronicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ResiduoEletronicoId"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("NUMBER(19)");

                    b.HasKey("ResiduoEletronicoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ResiduosEletronicos", (string)null);
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.UsuarioModel", b =>
                {
                    b.Property<long>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UsuarioId"));

                    b.Property<string>("Cep")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("NVARCHAR2(10)")
                        .HasDefaultValue("00000-000");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasDefaultValue("Cidade não informada");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(7)")
                        .HasDefaultValueSql("SYSDATE");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasDefaultValue("Endereço não informado");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .HasColumnType("NVARCHAR2(2)")
                        .HasDefaultValue("SP");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasDefaultValue("User");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)")
                        .HasDefaultValue("senha123");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)")
                        .HasDefaultValue("(00) 00000-0000");

                    b.HasKey("UsuarioId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.AlertaModel", b =>
                {
                    b.HasOne("apicoletalixoreciclavel.Models.UsuarioModel", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.ColetaModel", b =>
                {
                    b.HasOne("apicoletalixoreciclavel.Models.PontoColetaModel", "PontoColeta")
                        .WithMany("Coletas")
                        .HasForeignKey("PontoColetaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apicoletalixoreciclavel.Models.ResiduoEletronicoModel", "Residuo")
                        .WithMany()
                        .HasForeignKey("ResiduoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PontoColeta");

                    b.Navigation("Residuo");
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.NotificacaoModel", b =>
                {
                    b.HasOne("apicoletalixoreciclavel.Models.UsuarioModel", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.ResiduoEletronicoModel", b =>
                {
                    b.HasOne("apicoletalixoreciclavel.Models.UsuarioModel", "Usuario")
                        .WithMany("ResiduosEletronicos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.PontoColetaModel", b =>
                {
                    b.Navigation("Coletas");
                });

            modelBuilder.Entity("apicoletalixoreciclavel.Models.UsuarioModel", b =>
                {
                    b.Navigation("ResiduosEletronicos");
                });
#pragma warning restore 612, 618
        }
    }
}
