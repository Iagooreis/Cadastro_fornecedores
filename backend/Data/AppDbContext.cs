using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Fornecedor> Fornecedores { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.ToTable("Fornecedores");

            entity.HasKey(f => f.Id);

            entity.HasIndex(f => f.Cnpj)
                .IsUnique()
                .HasDatabaseName("IX_Fornecedores_Cnpj");

            entity.Property(f => f.Cnpj)
                .HasColumnType("VARCHAR(14)")
                .HasMaxLength(14)
                .IsRequired();

            entity.Property(f => f.RazaoSocial)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(f => f.NomeFantasia)
                .HasMaxLength(200);

            entity.Property(f => f.Email)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(f => f.Telefone)
                .HasMaxLength(20);

            entity.Property(f => f.Endereco)
                .HasMaxLength(300);

            entity.Property(f => f.Cidade)
                .HasMaxLength(100);

            entity.Property(f => f.Uf)
                .HasMaxLength(2);

            entity.Property(f => f.AtividadePrincipal)
                .HasMaxLength(300);

            entity.Property(f => f.SenhaHash)
                .IsRequired();

            entity.Property(f => f.SituacaoCadastral)
                .HasMaxLength(50);

            entity.Property(f => f.DataCadastro)
                .IsRequired();

            entity.Property(f => f.DataAtualizacao)
                .IsRequired();
        });
    }
}