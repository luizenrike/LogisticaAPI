using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.ApplicationServices.DataTransferObjects
{
    public class ProdutoUpdateRequest
    {
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public LocalizacaoRequest Localizacao { get; set; }
    }
}
