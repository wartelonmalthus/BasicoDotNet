using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;

namespace Bernhoeft.GRT.Teste.Api.Controllers.v1
{
    /// <response code="401">Não Autenticado.</response>
    /// <response code="403">Não Autorizado.</response>
    /// <response code="500">Erro Interno no Servidor.</response>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public class AvisosController : RestApiController
    {
        /// <summary>
        /// Retorna Todos os Avisos Cadastrados para Tela de Edição.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Lista com Todos os Avisos.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Sem Avisos.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<IEnumerable<GetAvisosResponse>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<object> GetAvisos([FromQuery] GetAvisosRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(request, cancellationToken);

        /// <summary>
        /// Retorna um Aviso por ID.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        /// <response code="404">Aviso Não Encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAvisoByIdResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> GetAviso([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new GetAvisoByIdRequest(id);
            return await Mediator.Send(request, cancellationToken);
        }

        /// <summary>
        /// Cria um novo Aviso.
        /// </summary>
        /// <param name="request">Dados do aviso a ser criado.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso criado.</returns>
        /// <response code="201">Criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IDocumentationRestResult<CreateAvisoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<object> PostAviso([FromBody] CreateAvisoRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(request, cancellationToken);

        /// <summary>
        /// Atualiza um Aviso existente.
        /// </summary>
        /// <param name="id">ID do aviso.</param>
        /// <param name="request">Dados atualizados do aviso.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso atualizado.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Aviso não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<UpdateAvisoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> PutAviso([FromRoute] int id, [FromBody] UpdateAvisoRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            return await Mediator.Send(request, cancellationToken);
        }

        /// <summary>
        /// Exclui (desativa) um Aviso.
        /// </summary>
        /// <param name="id">ID do aviso.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Confirmação de exclusão.</returns>
        /// <response code="200">Aviso excluído com sucesso.</response>
        /// <response code="404">Aviso não encontrado.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<DeleteAvisoResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> DeleteAviso([FromRoute] int id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAvisoRequest { Id = id }, cancellationToken);
    }
}