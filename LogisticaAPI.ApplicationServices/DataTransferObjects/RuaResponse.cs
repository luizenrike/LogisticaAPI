using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.ApplicationServices.DataTransferObjects
{
    public class RuaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<ProdutoRuaResponse> Produtos { get; set; }
       
    }

    public class ProdutoRuaResponse
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public string CategoriaProduto { get; set; }
    }
}
