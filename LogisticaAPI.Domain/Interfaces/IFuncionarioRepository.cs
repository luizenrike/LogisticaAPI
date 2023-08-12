using LogisticaAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Interfaces
{
    public interface IFuncionarioRepository : IBaseRepository<Funcionario> 
    {
         Funcionario GetLogin(string email, string senha);
         bool Exists(string email);
    }
}
