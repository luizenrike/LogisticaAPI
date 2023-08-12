using LogisticaAPI.Domain.Models;
using LogisticaAPI.Domain.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Produto GetById(int id);
        List<ProdutoResponse> GetAllProdutos();
        List<ProdutoResponse> GetFiltered(ProdutoFilterRequest filter);

    }
}
