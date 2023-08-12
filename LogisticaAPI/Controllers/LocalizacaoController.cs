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
    [Authorize]

    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;
        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista de todas as localizações.
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Lista das localizações.
        ///     
        /// Input: 
        /// 
        ///     Sem entrada de dados.
        /// 
        /// Output Expected:
        /// 
        ///     Saída contendo todos os produtos, exemplo:
        ///     
        ///     [
        ///               {
        ///                 "Id": 1,
        ///                 "Estante": "14",
        ///                 "Posicao": "2",
        ///                 }
        ///               },
        ///               {
        ///                 "Id": 2,
        ///                 "Estante": "10",
        ///                 "Posicao": "3",
        ///               }
        ///     ]
        ///
        /// Eventual Errors:
        /// 
        ///     Não há.
        ///
        /// </remarks>
        /// <response code="200">Ok</response>
        [ProducesResponseType(typeof(List<LocalizacaoResponse>), 200)]
        [HttpGet]
        public List<LocalizacaoResponse> GetAll() => _service.GetAll();

        /// <summary>
        /// Criação de uma localização
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Criação de uma localização 
        ///     
        ///     Obs¹: Conforme a regra de negócio, uma localização só pode ser criada ao adicionar um produto válido.
        ///     
        /// Input: 
        /// 
        ///     {
        ///       "Estante": 9,
        ///       "Posicao": 1,
        ///       "ProdutoId": 2
        ///     }
        /// 
        /// Output Expected:
        ///     
        ///     Status 201: Created.
        ///
        /// Eventual Errors:
        /// 
        ///     Se o produto informado não existe, ocorrerá o erro: O produto não foi encontrado, tente outro id de produto.
        ///     
        ///     Se a posição for menor igual a zero ou maior que três, ocorrerá o erro: Utilize uma posição maior que 0 e menor ou igual a 3 na estante.
        ///     
        ///     Se a estante for menor igual a zero ou maior que vinte, ocorrerá o erro: Utilize na estante um valor maior que 0 e menor ou igual a 20.
        ///     
        ///     Se a localização já existir ocorrerá o erro: Posição e estante já existem.
        ///     
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="404">ErrorException</response>
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [HttpPost]
        public async Task<IActionResult> CreateLocalizacao(LocalizacaoCreateRequest request)
        {
            await _service.CreateLocalizacao(request);
            return this.Created("Localizacao", request);
        }

        /// <summary>
        /// Atualização de uma localização
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Atualização de uma localização 
        ///     
        ///     Obs¹: Conforme a regra de negócio, uma localização só pode ser atualizada com a inserção de um produto válido.
        ///     
        /// Input: 
        /// 
        ///     {
        ///       "Estante": 9,
        ///       "Posicao": 1,
        ///       "ProdutoId": 2
        ///     }
        /// 
        /// Output Expected:
        ///     
        ///     Status 204: No Content
        ///
        /// Eventual Errors:
        /// 
        ///     Se o produto informado não existe, ocorrerá o erro: O produto não foi encontrado, tente outro id de produto.
        ///     
        ///     Se a posição for menor igual a zero ou maior que três, ocorrerá o erro: Utilize uma posição maior que 0 e menor ou igual a 3 na estante.
        ///     
        ///     Se a estante for menor igual a zero ou maior que vinte, ocorrerá o erro: Utilize na estante um valor maior que 0 e menor ou igual a 20.
        ///     
        ///     Se a localização não existir, ocorrerá o erro: O Id da localização informada não existe.
        ///
        /// </remarks>
        /// <response code="204">No Content</response>
        [ProducesResponseType(typeof(IActionResult), 204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateLocalizacao(LocalizacaoCreateRequest request, int Id)
        {
            await _service.UpdateLocalizacao(request, Id);
            return this.NoContent();
        }

        /// <summary>
        /// Remoção de uma localização
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Exclusão de uma localização baseada no id.
        ///     
        /// Input: 
        /// 
        /// 
        /// Output Expected:
        ///     
        ///     Status 204: No Content
        ///
        /// Eventual Errors:
        /// 
        ///     Se a localização não existir, ocorrerá o erro: O Id da localização informada não existe.
        ///
        /// </remarks>
        /// <response code="204">No Content</response>
        [ProducesResponseType(typeof(IActionResult), 204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteLocalizacao(int Id)
        {
            await _service.DeleteLocalizacao(Id);
            return this.NoContent();
        }
    }
}
