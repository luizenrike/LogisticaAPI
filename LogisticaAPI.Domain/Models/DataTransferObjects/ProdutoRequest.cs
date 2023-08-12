using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Models.DataTransferObjects
{
    public class ProdutoRequest
    {
        public string NomeProduto { get; set; }
        public string Categoria { get; set; }
        public string Rua { get; set; }
        public int Estante { get; set; }
        public int Posicao { get; set; }
    }
}
