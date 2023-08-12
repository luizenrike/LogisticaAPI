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
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;
        public LocalizacaoService(ILocalizacaoRepository repository, IMapper mapper, IProdutoRepository produtoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        public List<LocalizacaoResponse> GetAll()
        {
            var localizacoes = _repository.GetAllLocalizacoes();
            return _mapper.Map<List<LocalizacaoResponse>>(localizacoes);
        }

        public async Task CreateLocalizacao(LocalizacaoCreateRequest request)
        {
            var produto = _produtoRepository.GetById(request.ProdutoId);
            if(produto == null)
                throw new NotFoundException("O produto não foi encontrado, tente outro id de produto.");
            if (request.Posicao <= 0 || request.Posicao > 3)
                throw new NotFoundException("Utilize uma posição maior que 0 e menor ou igual a 3 na estante.");
            if(request.Estante <= 0 || request.Estante > 20)
                throw new NotFoundException("Utilize na estante um valor maior que 0 e menor ou igual a 20.");
            if (_repository.Exist(request.Posicao, request.Estante))
                throw new NotFoundException("Posição e estante já existem");

           

            var localizacao = new Localizacao()
            {
                Estante = request.Estante,
                Posicao = request.Posicao,
                ProdutoId = produto.Id
            };
            _repository.Insert(localizacao);

            await _repository.CompleteAsync();
        }
        
        public async Task UpdateLocalizacao(LocalizacaoCreateRequest request, int id)
        {
            var produto = _produtoRepository.GetById(request.ProdutoId);
            var localizacao = _repository.GetById(id);
            if (produto == null)
                throw new NotFoundException("O produto não foi encontrado, tente outro id de produto.");
            if (localizacao == null)
                throw new NotFoundException("O Id da localização informada não existe.");
            if (request.Posicao <= 0 || request.Posicao > 3)
                throw new NotFoundException("Utilize uma posição maior que 0 e menor ou igual a 3 na estante.");
            if (request.Estante <= 0 || request.Estante > 20)
                throw new NotFoundException("Utilize na estante um valor maior que 0 e menor ou igual a 20.");

            localizacao.Estante = request.Estante;
            localizacao.Posicao = request.Posicao;
            localizacao.ProdutoId = produto.Id;

            _repository.Update(localizacao);
            await _repository.CompleteAsync();

        }

        public async Task DeleteLocalizacao(int id)
        {
            var localizacao = _repository.GetById(id);
            if (localizacao == null)
                throw new NotFoundException("O Id da localização informada não existe.");
            _repository.Remove(localizacao);
            await _repository.CompleteAsync();

        }


    }
}
