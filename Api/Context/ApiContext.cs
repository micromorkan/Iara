using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Api.Context
{
    public partial class ApiContext : DbContext
    {
        public ApiContext()
        {
        }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cotacao> Cotacao { get; set; }
        public virtual DbSet<CotacaoItem> Cotacaoitem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //O correto seria a aplicação buscar a string de conexão no appsettings, contudo devido ao banco ser um MDF, deixer aqui no código para que o diretorio seja ajustado automaticamente.
                //Foi necessário validar a configuração do MDF para execução dos testes unitários
                var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string mdfPath = Path.GetFullPath(Path.Combine(directoryPath, "..//..//..//..//Api//Database"));
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + mdfPath + "\\iara.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cotacao>(entity =>
            {
                entity.ToTable("cotacao");

                entity.Property(e => e.Bairro).HasMaxLength(50);

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasColumnName("CEP")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cnpjcomprador)
                    .IsRequired()
                    .HasColumnName("CNPJComprador")
                    .HasMaxLength(20);

                entity.Property(e => e.Cnpjfornecedor)
                    .IsRequired()
                    .HasColumnName("CNPJFornecedor")
                    .HasMaxLength(20);

                entity.Property(e => e.Complemento).HasMaxLength(200);

                entity.Property(e => e.DataCotacao).HasColumnType("datetime");

                entity.Property(e => e.DataEntregaCotacao).HasColumnType("datetime");

                entity.Property(e => e.Logradouro).HasMaxLength(200);

                entity.Property(e => e.Observacao).HasMaxLength(999);

                entity.Property(e => e.Uf)
                    .HasColumnName("UF")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CotacaoItem>(entity =>
            {
                entity.ToTable("cotacaoitem");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Marca).HasMaxLength(200);

                entity.Property(e => e.Preco).HasColumnType("money");

                entity.Property(e => e.Unidade).HasMaxLength(200);

                entity.HasOne(d => d.Cotacao)
                    .WithMany(p => p.CotacaoItems)
                    .HasForeignKey(d => d.IdCotacao)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__cotacaoit__IdCot__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
