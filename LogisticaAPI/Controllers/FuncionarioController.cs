using LogisticaAPI.ApplicationServices.DataTransferObjects;
using LogisticaAPI.ApplicationServices.Services;
using LogisticaAPI.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _service;
        public FuncionarioController(FuncionarioService service)
        {
            _service = service;
        }

        /// <summary>
        /// Login para utilizar os outros serviços
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     O usuário deve fazer um login com um funcionário cadastrado e após a autenticação ser um sucesso, ele receberá um token de validação.
        ///     
        /// Input: 
        /// 
        ///     (1) Email:
        ///     
        ///         Ex: admin@logitisca.com
        ///         
        ///     (2) Senha:
        ///     
        ///         Ex: admin
        /// 
        /// Output Expected:
        /// 
        ///     Token de validação que deverá ser utilizado acompanhado do Bearer para que o login seja efetuado, ou seja: Bearer TokenValidação.
        ///     
        ///     
        ///
        /// Eventual Errors:
        /// 
        ///     Caso o usuário ou a senha estejam incorretos, ocorrerá o erro: O usuário ou senha incorreta.
        ///
        /// </remarks>
        /// <response code="200">Ok</response>
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [HttpGet]
        [Route("login")]
        public Task<string> GetLogin([FromQuery]string email, [FromQuery] string senha) => _service.GetLogin(email, senha);

        /// <summary>
        /// Criação de um novo funcionário
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Criação de um novo funcionário
        ///     
        /// Input: 
        /// 
        ///     {
        ///       "Nome": supervisor,
        ///       "Email": supervisor@logistica.com,
        ///       "Senha": supervisor2023
        ///     }
        /// 
        /// Output Expected:
        ///     
        ///     Status 201: Created.
        ///
        /// Eventual Errors:
        /// 
        ///     Se já existir um usuário com o respectivo email, ocorrerá o erro: O email informado já pertece a um usuário.
        ///     
        ///     Caso sejam inseridos valores nulos nos campos nome, email e senha, ocorrerá o erro: O usuário não pode conter nome, email ou senha nulos.
        ///     
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="404">ErrorExpection</response>
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateFuncionario(FuncionarioRequest request)
        {
            await _service.CreateFuncionario(request);
            return this.Created("Funcionario", request);
        }
        
    }
}
