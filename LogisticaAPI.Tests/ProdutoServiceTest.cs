using AutoMapper;
using LogisticaAPI.ApplicationServices.DataTransferObjects;
using LogisticaAPI.ApplicationServices.Profiles;
using LogisticaAPI.ApplicationServices.Services;
using LogisticaAPI.Domain.Interfaces;
using LogisticaAPI.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LogisticaAPI.UnitTests
{
    public class ProdutoServiceTest
    {
        private readonly Mock<IProdutoRepository> _repository;
        private readonly Mock<IRuaRepository> _ruaRepository;
        private readonly Mock<ILocalizacaoRepository> _localizacaoRepository;
        private readonly ProdutoService _sut;

        public ProdutoServiceTest()
        {
            _repository = new Mock<IProdutoRepository>();
            _ruaRepository = new Mock<IRuaRepository>();
            _localizacaoRepository = new Mock<ILocalizacaoRepository>();
            var mapper = new MapperConfiguration(x => { x.AddProfile(new ProdutoProfile()); x.AddProfile(new RuaProfile()); x.AddProfile(new LocalizacaoProfile());})
                .CreateMapper();
            _sut = new ProdutoService(_repository.Object, _ruaRepository.Object, _localizacaoRepository.Object, mapper);
        }


        [Fact]
        public void GetAllProdutos_When_Return_Ok()
        {
            // Arrange
            var rua = new Rua()
            {
                Id = 1,
                Nome = "A"
            };
            var produto = new Produto()
            {
                Id = 1,
                ProdutoNome = "teste",
                Categoria = "teste",
                RuaId = 1,
                Rua = rua
            };

            var produtoResponse = new Domain.Models.DataTransferObjects.ProdutoResponse()
            {
                Id = produto.Id,
                Nome = produto.ProdutoNome,
                Categoria = produto.Categoria,
                Localizacao = new Domain.Models.DataTransferObjects.LocalizacaoResponse()
                {
                    Rua = rua.Nome,
                    Estante = 0,
                    Posicao = 0
                }
            };

            var produtos = new List<Domain.Models.DataTransferObjects.ProdutoResponse>()
            {
                produtoResponse
            };

            _repository.Setup(x => x.GetAllProdutos()).Returns(produtos);

            // Act
            var result = _sut.GetAll();

            // Assert
            _repository.Verify(x => x.GetAllProdutos(), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async void CreateProduto_When_Return_Ok()
        {
            // Arrange
            var rua = new Rua()
            {
                Id = 1,
                Nome = "A"
            };
           
            var produtoRequest = new ProdutoRequest()
            {
                NomeProduto = "Teste",
                Categoria = "Teste",
                Localizacao = new LocalizacaoRequest()
                {
                    RuaId = 1
                }
            };

            _ruaRepository.Setup(x => x.GetById(rua.Id)).Returns(rua);

            // Act

            await _sut.CreateProduto(produtoRequest);

            // Assert
            _repository
                .Verify(x => x.Insert(It.Is<Produto>(p => p.ProdutoNome == produtoRequest.NomeProduto)), Times.Once());
        }

        [Fact]
        public async void UpdateProduto_When_Return_Ok()
        {
            // Arrange
            var rua = new Rua()
            {
                Id = 1,
                Nome = "A"
            };

            var produto = new Produto()
            {
                Id = 1,
                ProdutoNome = "Produto Criado",
                Categoria = "Categoria criada",
                RuaId = 1,
                Rua = rua
            };
            
            var produtoRequest = new ProdutoUpdateRequest()
            {
               Nome= "Teste",
                Categoria = "Teste",
                Localizacao = new LocalizacaoRequest()
                {
                    RuaId = 1
                }
            };
            var id = 1;

            _ruaRepository.Setup(x => x.GetById(rua.Id)).Returns(rua);
            _repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(produto);

            // Act

            await _sut.UpdateProduto(produtoRequest, id);

            // Assert
            _repository
                .Verify(x => x.Update(produto), Times.Once());
        }

        [Fact]
        public async void DeleteProduto_When_Return_Ok()
        {

            // Arrange

            var rua = new Rua()
            {
                Id = 1,
                Nome = "A"
            };

            var produto = new Produto()
            {
                Id = 1,
                ProdutoNome = "Produto Criado",
                Categoria = "Categoria criada",
                RuaId = 1,
                Rua = rua
            };

            _repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(produto);

            // Act
            var id = 1;
            await _sut.DeleteProduto(id);

            // Assert
            _repository
                .Verify(x => x.Remove(produto), Times.Once());
        }
    }
}
