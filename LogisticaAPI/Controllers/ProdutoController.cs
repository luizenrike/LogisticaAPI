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
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// Listagem de todos os produtos armazenados no estoque.
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Lista de todos os produtos armazenados com: nome, categoria e localização.
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
        ///                 "id": 1,
        ///                 "nome": "Pneus",
        ///                 "categoria": "Automotivo",
        ///                 "localizacao": {
        ///                   "rua": "A",
        ///                   "estante": 0,
        ///                   "posicao": 0
        ///                 }
        ///               },
        ///               {
        ///                 "id": 2,
        ///                 "nome": "Notebook Asus",
        ///                 "categoria": "Computadores",
        ///                 "localizacao": {
        ///                   "rua": "B",
        ///                   "estante": 0,
        ///                   "posicao": 0
        ///                 }
        ///               }
        ///     ]
        ///
        /// Eventual Errors:
        /// 
        ///     Não há.
        ///
        /// </remarks>
        /// <response code="200">Ok</response>
        [ProducesResponseType(typeof(List<ProdutoResponse>), 200)]
        [HttpGet]
        public List<ProdutoResponse> GetAll() => _produtoService.GetAll();

        /// <summary>
        /// Listagem de produtos filtrada.
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Lista de todos os produtos com a aplicação dos filtros.
        ///     
        /// Input: 
        /// 
        ///     (1) FiltroNome:
        ///         
        ///         Ex: Notebook Asus
        ///         
        ///     (2) FiltroCategoria
        ///     
        ///         Ex: Computadores
        /// 
        /// Output Expected:
        /// 
        ///     Saída contendo todos os produtos filtrados, exemplo:
        ///     
        ///     [
        ///               {
        ///                 "id": 3,
        ///                 "nome": "Notebook Asus",
        ///                 "categoria": "Computadores",
        ///                 "localizacao": {
        ///                   "rua": "A",
        ///                   "estante": 0,
        ///                   "posicao": 0
        ///                 }
        ///               },
        ///               {
        ///                 "id": 2,
        ///                 "nome": "Notebook Asus",
        ///                 "categoria": "Computadores",
        ///                 "localizacao": {
        ///                   "rua": "B",
        ///                   "estante": 0,
        ///                   "posicao": 0
        ///                 }
        ///               }
        ///     ]
        ///
        /// Eventual Errors:
        /// 
        ///     Não há.
        ///
        /// </remarks>
        /// <response code="200">Ok</response>
        [ProducesResponseType(typeof(List<ProdutoResponse>), 200)]
        [HttpGet]
        [Route("filtered")]
        public List<ProdutoResponse> GetFiltered([FromQuery] ProdutoFilterRequest filter) => _produtoService.GetFiltered(filter);

        /// <summary>
        /// Criação de um produto
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Criação de um produto contendo: Nome, Categoria e Localização. 
        ///     
        ///     Obs¹: Conforme a regra de negócio, um produto ao ser criado deve obrigatoriamente possuir no mínimo uma rua válida e existente. Em relação a localização(estante e posição na estante)  ela não é obrigatória no momento.
        ///     
        /// Input: 
        /// 
        ///     {
        ///       "nomeProduto": "Notebook Asus",
        ///       "categoria": "Computadores",
        ///       "localizacao": {
        ///         "ruaId": 2,
        ///         "localizacaoId": 1,
        ///         "estante": 12,
        ///         "posicao": 1
        ///       }
        ///     }
        /// 
        /// Output Expected:
        ///     
        ///     Status 201: Created.
        ///
        /// Eventual Errors:
        /// 
        ///     Se a rua informada não existe, o produto não pode ser adicionado, dessa forma ocorrerá o erro: A rua não existe, informe uma rua válida.
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [HttpPost]
        public async Task<IActionResult> CreateProduto(ProdutoRequest request)
        {
            await _produtoService.CreateProduto(request);
            return this.Created("Produto", request);
        }

        /// <summary>
        /// Atualização de um produto existente
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Atualização das informações de um produto.
        ///     
        /// Input: 
        /// 
        ///     (1) Id do produto
        ///             
        ///             Ex: 1
        ///           
        ///     (2) Request Body: contendo as informações de atualização, exemplo:
        /// 
        ///             {
        ///               "nomeProduto": "Geladeira",
        ///               "categoria": "Eletrodoméstico",
        ///               "localizacao": {
        ///                 "ruaId": 2,
        ///                 "localizacaoId": 0,
        ///                 "estante": 0,
        ///                 "posicao": 0
        ///               }
        ///             }
        /// 
        /// Output Expected:
        ///     
        ///     Status 204: No Content.
        ///
        /// Eventual Errors:
        /// 
        ///     Se nenhum produto existir com o Id informado, ocorrerá o erro: O produto não foi encontrado, tente outro id de produto.    
        /// 
        ///     Se a rua informada não existe, o produto não pode ser adicionado, dessa forma ocorrerá o erro: A rua não existe, informe uma rua válida.
        ///     
        ///
        /// </remarks>
        /// <response code="204">No Content</response>
        [ProducesResponseType(typeof(IActionResult), 204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateProduto(ProdutoUpdateRequest request, int Id)
        {
            await _produtoService.UpdateProduto(request, Id);
            return this.NoContent();
        }

        /// <summary>
        /// Remoção de produto do banco de dados.
        /// </summary>
        /// <remarks>
        /// Description:
        /// 
        ///     Exclusão de um produto através do seu id.
        ///     
        /// Input: 
        /// 
        ///     (1) Id do produto
        ///             
        ///             Ex: 1
        ///           
        /// 
        /// Output Expected:
        ///     
        ///     Status 204: No Content.
        ///
        /// Eventual Errors:
        /// 
        ///     Se nenhum produto existir com o Id informado, ocorrerá o erro: O produto não foi encontrado, tente outro id de produto.    
        /// 
        /// </remarks>
        /// <response code="204">No Content</response>
        [ProducesResponseType(typeof(IActionResult), 204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigException))]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduto(int Id)
        {
            await _produtoService.DeleteProduto(Id);
            return this.NoContent();
        }


    }
}
