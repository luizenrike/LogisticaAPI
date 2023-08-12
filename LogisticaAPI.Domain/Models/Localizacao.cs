using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Models
{
    public class Localizacao : BaseEntity
    {
        public int Estante { get; set; }
        public int Posicao { get; set; }
        public int? ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
