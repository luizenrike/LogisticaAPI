using LogisticaAPI.Domain.Models;
using LogisticaAPI.Domain.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Interfaces
{
    public interface ILocalizacaoRepository : IBaseRepository<Localizacao>
    {
        Localizacao GetById(int id);
        List<LocalizacaoResponse> GetAllLocalizacoes();
        bool Exist(int estante, int posicao);
    }
}
