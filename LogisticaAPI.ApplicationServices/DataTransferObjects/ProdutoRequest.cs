
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.ApplicationServices.DataTransferObjects
{
    public class ProdutoRequest
    {
        public string NomeProduto { get; set; }
        public string Categoria { get; set; }
        public LocalizacaoRequest Localizacao { get; set; }
    }
}
