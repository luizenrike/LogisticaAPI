using AutoMapper;
using LogisticaAPI.ApplicationServices.DataTransferObjects;
using LogisticaAPI.ApplicationServices.Exceptions;
using LogisticaAPI.Domain.Interfaces;
using LogisticaAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.ApplicationServices.Services
{
    public class RuaService
    {
        private readonly IRuaRepository _repository;
        private readonly IMapper _mapper;
        public RuaService(IRuaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<RuaDataResponse> GetAllRuas()
        {
            var ruas = _repository.GetAllRuas();
            var result = _mapper.Map<List<RuaDataResponse>>(ruas);
            return result;
        }

        public List<RuaResponse> GetRuasProd()
        {
            var ruasProdutos = _repository.GetRuasProd();
            var result = _mapper.Map<List<RuaResponse>>(ruasProdutos);
            return result;
        }

        public async Task CreateRua(RuaRequest request)
        {
            if (_repository.Exist(request.NomeRua))
                throw new NotFoundException("Já existe uma rua com esse nome.");

            var rua = new Rua()
            {
                Nome = request.NomeRua
            };
            _repository.Insert(rua);
            await _repository.CompleteAsync();
        }

        public async Task UpdateRua(RuaRequest request, int id)
        {
            var rua = _repository.GetById(id);
            if (rua == null)
                throw new NotFoundException("A rua informada não existe, tente outro Id.");

            rua.Nome = request.NomeRua;
            _repository.Update(rua);
            await _repository.CompleteAsync();
        }

        public async Task DeleteRua(int id)
        {
            var rua = _repository.GetById(id);
            if (rua == null)
                throw new NotFoundException("A rua informada não existe, tente outro Id.");
            _repository.Remove(rua);

            await _repository.CompleteAsync();

        }



    }
}
