using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Models.DataTransferObjects
{
    public class ProdutoFilterRequest 
    {
        public string? FiltroNome { get; set; }
        public string? FiltroCategoria { get; set; }

    }
}
