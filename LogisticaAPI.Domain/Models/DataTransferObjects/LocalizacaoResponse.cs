using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Models.DataTransferObjects
{
    public class LocalizacaoResponse
    {
        public int Id { get; set; }
        public int Estante { get; set; }
        public int Posicao { get; set; }
        public string Rua { get; set; }
    }
}
