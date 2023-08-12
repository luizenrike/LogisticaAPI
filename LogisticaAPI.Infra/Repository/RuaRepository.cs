using LogisticaAPI.Domain.Interfaces;
using LogisticaAPI.Domain.Models;
using LogisticaAPI.Domain.Models.DataTransferObjects;
using LogisticaAPI.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Infra.Repository
{
    public class RuaRepository : BaseRepository<Rua>, IRuaRepository
    {
        public RuaRepository(UnitOfWork context) : base(context)
        {

        }

        public Rua GetById(int id)
        {
            var result = dataBase.Ruas.Where(r => r.Id == id).FirstOrDefault();
            return result;
        }

        public List<RuaDataResponse> GetAllRuas()
        {
            var result = dataBase.Ruas.Select(r => new RuaDataResponse
            {
                Id = r.Id,
                Nome = r.Nome
            }).ToList();
            return result;
        }

        public List<RuaResponse> GetRuasProd()
        {
            var result = dataBase.Ruas.Include(r => r.Produtos)
                .Select(r => new RuaResponse
                {
                    Id = r.Id,
                    Nome = r.Nome,
                    Produtos = r.Produtos.Select(p => new ProdutoRuaResponse
                    {
                        Id = p.Id,
                        NomeProduto = p.ProdutoNome,
                        CategoriaProduto = p.Categoria
                    }).ToList()
                }).ToList();

            return result;
        }

        public bool Exist(string nome)
        {
            return dataBase.Ruas.Any(r => r.Nome.ToUpper() == nome.ToUpper());
        }
    }
}
