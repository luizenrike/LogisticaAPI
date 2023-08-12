using LogisticaAPI.ApplicationServices.DataTransferObjects;
using LogisticaAPI.ApplicationServices.Services;
using LogisticaAPI.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace LogisticaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RuaController : ControllerBase
    {
        private readonly RuaService _ruaService;
        public RuaController(RuaService ruaService)
        {
            _ruaService = ruaService;
        }

        /// <summary>
        /// Listagem das ruas
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Lista de todas ruas contidas no banco de dados
        ///     
        /// Input: 
        /// 
        ///     Sem entrada de dados.
        /// 
        /// Output Expected:
        /// 
        ///     Saída contendo todas as ruas, exemplo:
        ///     
        ///     [
        ///               {
        ///                 "id": 1,
        ///                 "nome": "A",
        ///               },
        ///               {
        ///                 "id": 2,
        ///                 "nome": "B",
        ///               }
        ///     ]
        ///
        /// Eventual Errors:
        /// 
        ///     Não há.
        ///
        /// </remarks>
        /// <response code="200">Ok</response>
        [ProducesResponseType(typeof(List<RuaDataResponse>), 200)]
        [HttpGet]
        public List<RuaDataResponse> GetAllRuas() => _ruaService.GetAllRuas();


        /// <summary>
        /// Listagem das ruas e produtos
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Lista de todas ruas e todos os produtos contidos em cada rua
        ///     
        /// Input: 
        /// 
        ///     Sem entrada de dados.
        /// 
        /// Output Expected:
        /// 
        ///     Saída contendo todas as ruas e os produtos armazenados nela, exemplo:
        ///     
        ///        [
        ///          {
        ///              "id": 1,
        ///              "nome": "A",
        ///              "produtos": [
        ///                  {
        ///                      "id": 1,
        ///                      "nomeProduto": "Pneus",
        ///                      "categoriaProduto": "Automotivo"
        ///                  }
        ///              ]
        ///          },
        ///          {
        ///              "id": 2,
        ///              "nome": "B",
        ///              "produtos": [
        ///                  {
        ///                      "id": 2,
        ///                      "nomeProduto": "Notebook Asus",
        ///                      "categoriaProduto": "Computadores"
        ///                  },
        ///                  {
        ///                      "id": 3,
        ///                      "nomeProduto": "Notebook Acer",
        ///                      "categoriaProduto": "Computadores"
        ///                  }
        ///              ]
        ///          }
        ///      ]
        ///
        /// Eventual Errors:
        /// 
        ///     Não há.
        ///
        /// </remarks>
        /// <response code="200">Ok</response>
        [ProducesResponseType(typeof(List<RuaResponse>), 200)]
        [Route("ruas-e-produtos")]
        [HttpGet]
        public List<RuaResponse> GetRuasProd() => _ruaService.GetRuasProd();

        /// <summary>
        /// Criação de uma nova rua.
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Criação de uma nova rua através do nome. 
        ///     
        /// Input: 
        /// 
        ///     {
        ///       "nomeRua": "B",
        ///     }
        /// 
        /// Output Expected:
        ///     
        ///     Status 201: Created.
        ///
        /// Eventual Errors:
        /// 
        ///     Se o nome da rua informada já existir, ocorrerá o erro: Já existe uma rua com esse nome.
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [HttpPost]
        public async Task<IActionResult> CreateRua(RuaRequest request)
        {
            await _ruaService.CreateRua(request);
            return this.Created("Rua", request);
        }

        /// <summary>
        /// Atualização de uma rua.
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Atualização do nome de uma rua. 
        ///     
        /// Input: 
        ///     
        ///     (1) Id:
        ///         
        ///         Ex: 2
        ///      
        ///     (2) FromBody:
        ///     
        ///         {
        ///             "nomeRua": "F",
        ///         }
        /// 
        /// Output Expected:
        ///     
        ///     Status 204: No Content
        ///
        /// Eventual Errors:
        /// 
        ///     Se o Id da rua informada não existir, ocorrerá o erro: A rua informada não existe, tente outro Id.
        ///
        /// </remarks>
        /// <response code="204">No Content</response>
        [ProducesResponseType(typeof(IActionResult), 204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateRua(RuaRequest request, int Id)
        {
            await _ruaService.UpdateRua(request, Id);
            return this.NoContent();
        }

        /// <summary>
        /// Remoção de uma rua.
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Exclusão de uma rua através do Id.
        ///     
        /// Input: 
        /// 
        ///         (1) Id:
        ///     
        ///             Ex: 2
        /// 
        /// 
        /// Output Expected:
        ///     
        ///     Status 204: No Content
        ///
        /// Eventual Errors:
        /// 
        ///     Se o Id da rua informada não existir, ocorrerá o erro: A rua informada não existe, tente outro Id.
        ///
        /// </remarks>
        /// <response code="204">No Content</response>
        [ProducesResponseType(typeof(IActionResult), 204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> UpdateRua(int Id)
        {
            await _ruaService.DeleteRua(Id);
            return this.NoContent();
        }

    }
}
