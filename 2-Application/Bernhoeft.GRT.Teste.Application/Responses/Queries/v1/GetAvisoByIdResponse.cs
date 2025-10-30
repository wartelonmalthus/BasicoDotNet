using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;

namespace Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;

public class GetAvisoByIdResponse : GetAvisosResponse
{
    public static implicit operator GetAvisoByIdResponse(AvisoEntity entity) => new()
    {
        Id = entity.Id,
        Ativo = entity.Ativo,
        Titulo = entity.Titulo,
        Mensagem = entity.Mensagem,
        DataCriacao = entity.DataCriacao,
        DataAlteracao = entity.DataAlteracao
    };
}
