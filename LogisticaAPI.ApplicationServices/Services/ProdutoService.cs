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
    public class ProdutoService 
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRuaRepository _ruaRepository;
        private readonly ILocalizacaoRepository _localizacaoRepository;
        public ProdutoService(IProdutoRepository repository, IRuaRepository ruaRepository, ILocalizacaoRepository localizacaoRepository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _ruaRepository = ruaRepository;
            _localizacaoRepository = localizacaoRepository;
        }

        public List<ProdutoResponse> GetAll()
        {

            var produtos = _repository.GetAllProdutos();
            return _mapper.Map<List<ProdutoResponse>>(produtos);
        }

        public List<ProdutoResponse> GetFiltered(ProdutoFilterRequest filter)
        {
            var filterMapped = _mapper.Map<Domain.Models.DataTransferObjects.ProdutoFilterRequest>(filter);
            var produtos = _repository.GetFiltered(filterMapped);
            return _mapper.Map<List<ProdutoResponse>>(produtos);

        }

        public async Task CreateProduto(ProdutoRequest request)
        {
            var rua = _ruaRepository.GetById(request.Localizacao.RuaId);
            var localizacao = _localizacaoRepository.GetById(request.Localizacao.LocalizacaoId);

            if (rua == null)
                throw new NotFoundException("A rua não existe, informe uma rua válida.");

            var produto = new Produto()
            {
                ProdutoNome = request.NomeProduto,
                Categoria = request.Categoria,
                DataArmazenamento = DateTime.Now,
                RuaId = request.Localizacao.RuaId,
                Rua = rua,
                Localizacao = localizacao != null ? localizacao : null
            };
            _repository.Insert(produto);
            await _repository.CompleteAsync();

        }

        public async Task UpdateProduto(ProdutoUpdateRequest request, int id)
        {
            var produto = _repository.GetById(id);
            var rua = _ruaRepository.GetById(request.Localizacao.RuaId);
            var localizacao = _localizacaoRepository.GetById(request.Localizacao.LocalizacaoId);
            if (produto == null)
                throw new NotFoundException("O produto não foi encontrado, tente outro id de produto.");
            if(rua == null)
                throw new NotFoundException("A rua não existe, informe uma rua válida.");

            produto.ProdutoNome = request.Nome;
            produto.Categoria = String.IsNullOrEmpty(request.Categoria) ? produto.Categoria : request.Categoria;
            produto.Rua = rua;
            produto.Localizacao = localizacao!= null ? localizacao : produto.Localizacao;
            produto.RuaId = rua.Id;

            _repository.Update(produto);
            await _repository.CompleteAsync();

        }

        public async Task DeleteProduto(int id)
        {
            var produto = _repository.GetById(id);
            if (produto == null)
                throw new NotFoundException("O produto não foi encontrado, tente outro id de produto.");

            _repository.Remove(produto);
            await _repository.CompleteAsync();

        }




    }
}
