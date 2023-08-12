using LogisticaAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Infra.Context
{
    public class UnitOfWork : DbContext
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> context) : base(context)
        {

        }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<Rua>().HasMany(r => r.Produtos).WithOne(p => p.Rua).HasForeignKey(p => p.RuaId);
            Builder.Entity<Produto>().HasOne(p => p.Localizacao).WithOne(l => l.Produto).HasForeignKey<Localizacao>(l => l.ProdutoId);

            Builder.Entity<Funcionario>().HasKey(f => f.Id);
            Builder.Entity<Produto>().HasKey(p => p.Id);

            Builder.Entity<Funcionario>().HasData(new Funcionario
            {
                Id = 1,
                Nome = "admin",
                Senha = "admin",
                Email = "admin@logistica.com"
            });

            Builder.Entity<Rua>().HasData(new Rua
            {
                Id = 1,
                Nome = "A"
            });

            Builder.Entity<Produto>().HasData(new Produto
            {
                Id = 1,
                ProdutoNome = "Pneus",
                Categoria = "Automotiva",
                DataArmazenamento = DateTime.Now.Date,
                RuaId = 1
            });

            Builder.Entity<Localizacao>().HasData(new Localizacao
            {
                Id = 1,
                Estante = 10,
                Posicao = 2,
                ProdutoId = 1
            });
        }

        // criando as tabelas do banco:
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Rua> Ruas { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
    }
}
