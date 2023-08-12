using LogisticaAPI.Domain.Models;
using LogisticaAPI.Domain.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Interfaces
{
    public interface IRuaRepository : IBaseRepository<Rua>
    {
        Rua GetById(int id);
        List<RuaDataResponse> GetAllRuas();
        List<RuaResponse> GetRuasProd();
        bool Exist(string nome);
    }
}
