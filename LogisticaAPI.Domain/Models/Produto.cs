using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Models
{
    public class Produto : BaseEntity
    {
        public string ProdutoNome { get; set; }
        public string Categoria { get; set; }
        public DateTime? DataArmazenamento{ get; set; }
        public int RuaId { get; set; }
        public Localizacao Localizacao { get; set; }
        public Rua Rua { get; set; }
    }
}
