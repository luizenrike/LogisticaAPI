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
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(UnitOfWork context) : base(context)
        {

        }

        public List<ProdutoResponse> GetAllProdutos()
        {
            var produtos = dataBase.Produtos
                .Include(p => p.Rua)
                .Include(p => p.Localizacao)
                .Select(p => new ProdutoResponse
                {
                    Id = p.Id,
                    Nome = p.ProdutoNome,
                    Categoria = p.Categoria,
                    DataArmazenamento = p.DataArmazenamento.Value.Date,
                    Localizacao = new LocalizacaoResponse
                    {
                        Id = p.Localizacao != null ? p.Localizacao.Id : 0,
                        Rua = p.Rua.Nome,
                        Estante = p.Localizacao != null ? p.Localizacao.Estante : 0,
                        Posicao = p.Localizacao != null ? p.Localizacao.Posicao : 0,

                    }
                })
                .ToList();

            return produtos;


        }

        public Produto GetById(int id)
        {
            var produto = dataBase.Produtos.FirstOrDefault(p => p.Id == id);
            return produto;
        }

        public List<ProdutoResponse> GetFiltered(ProdutoFilterRequest filter)
        {

            var result = dataBase.Produtos
                .Include(p => p.Rua)
                .Include(p => p.Localizacao)
                .Where(p =>
                    (string.IsNullOrEmpty(filter.FiltroNome) || p.ProdutoNome.Contains(filter.FiltroNome)) &&
                    (string.IsNullOrEmpty(filter.FiltroCategoria) || p.Categoria.Contains(filter.FiltroCategoria)) ||
                    (string.IsNullOrEmpty(filter.FiltroNome) && string.IsNullOrEmpty(filter.FiltroCategoria)))       
                .Select(p => new ProdutoResponse
                    {
                        Id = p.Id,
                        Nome = p.ProdutoNome,
                        Categoria = p.Categoria,
                        DataArmazenamento = p.DataArmazenamento.Value.Date,
                        Localizacao = new LocalizacaoResponse
                        {
                            Id = p.Localizacao != null ? p.Localizacao.Id : 0,
                            Rua = p.Rua.Nome,
                            Estante = p.Localizacao != null ? p.Localizacao.Estante : 0,
                            Posicao = p.Localizacao != null ? p.Localizacao.Posicao : 0,
                        }
                    }).ToList();

            return result;
        }

    }
}
