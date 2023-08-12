
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.ApplicationServices.DataTransferObjects
{
    public class LocalizacaoRequest 
    {
        public int RuaId { get; set; }
        public int LocalizacaoId { get; set; }
        public int Estante { get; set; }
        public int Posicao { get; set; }
    }
}
