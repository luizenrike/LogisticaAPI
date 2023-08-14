using AutoMapper;
using LogisticaAPI.ApplicationServices.DataTransferObjects;
using LogisticaAPI.ApplicationServices.Exceptions;
using LogisticaAPI.Domain.Interfaces;
using LogisticaAPI.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.ApplicationServices.Services
{
    public class FuncionarioService
    {
        private readonly IFuncionarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public FuncionarioService(IFuncionarioRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> GetLogin(string email, string senha)
        {
            var funcionario = _repository.GetLogin(email, senha);
            if(funcionario != null)
                return GenerateToken(funcionario);
            throw new NotFoundException("O usuário ou senha incorreta");
        }

        public async Task CreateFuncionario(FuncionarioRequest request)
        {
            if (_repository.Exists(request.Email))
                throw new NotFoundException("O email informado já pertence a outro usuário.");
            if (string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
                throw new NotFoundException("O usuário não pode conter nome, email ou senha nulos");

            var funcionario = new Funcionario()
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha
            };

            _repository.Insert(funcionario);

            await _repository.CompleteAsync();
        }



        public string GenerateToken(Funcionario funcionario)
        {
            string SecretKeyConfig = _configuration.GetSection("secretKey").Value;
            byte[] secretKey = Encoding.ASCII.GetBytes(SecretKeyConfig);
            var tokenHandler = new JwtSecurityTokenHandler();


            // adicionando permissões no Token:
            var PermissaoNome = new Claim("Nome", funcionario.Nome);
            List<Claim> Permissoes = new List<Claim>();
            Permissoes.Add(PermissaoNome);
            var Claims = new ClaimsIdentity(Permissoes);

            var TokenDescriptor = new SecurityTokenDescriptor();
            TokenDescriptor.Subject = Claims;

            TokenDescriptor.Expires = DateTime.Now.AddHours(8); 

            TokenDescriptor.Issuer = funcionario.Nome;
            TokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            string Token = tokenHandler.CreateEncodedJwt(TokenDescriptor);
            return Token;
        }
    }
}
