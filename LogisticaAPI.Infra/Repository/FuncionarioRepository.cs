using LogisticaAPI.Domain.Interfaces;
using LogisticaAPI.Domain.Models;
using LogisticaAPI.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Infra.Repository
{
    public class FuncionarioRepository : BaseRepository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(UnitOfWork context) : base(context)
        {

        }
        public Funcionario GetLogin(string email, string senha)
        {
            var funcionario = dataBase.Funcionarios.Where(f => f.Email.ToUpper() == email.ToUpper() && f.Senha == senha).FirstOrDefault();
            return funcionario;
        }

        public bool Exists(string email)
        {
            return dataBase.Funcionarios.Any(f => f.Email.ToUpper() == email.ToUpper());
        }
    }
}
