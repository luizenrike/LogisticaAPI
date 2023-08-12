using LogisticaAPI.Domain.Interfaces;
using LogisticaAPI.Domain.Models;
using LogisticaAPI.Domain.Models.DataTransferObjects;
using LogisticaAPI.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Infra.Repository
{
    public class LocalizacaoRepository : BaseRepository<Localizacao>, ILocalizacaoRepository
    {
        public LocalizacaoRepository(UnitOfWork context) : base(context)
        {

        }

        public Localizacao GetById(int id)
        {
            var result = dataBase.Localizacoes.Include(p => p.Produto).Where(l => l.Id == id).FirstOrDefault();
            return result;
        }

        public List<LocalizacaoResponse> GetAllLocalizacoes()
        {
            var result = dataBase.Localizacoes
                .Select(l => new LocalizacaoResponse
                    {
                        Id = l.Id,
                        Estante = l.Estante,
                        Posicao = l.Posicao
                    }).ToList();

            return result;
        }

        public bool Exist(int estante, int posicao)
        {
            return dataBase.Localizacoes.Any(l => l.Posicao == posicao && l.Estante == estante);
        }
    }
}
