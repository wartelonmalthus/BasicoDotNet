using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;

namespace Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;

public class UpdateAvisoResponse
{
    public int Id { get; set; }
    public string Titulo { get; set; } 
    public string Mensagem { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public bool Ativo { get; set; }

    public static implicit operator UpdateAvisoResponse(AvisoEntity e) => new()
    {
        Id = e.Id,
        Titulo = e.Titulo,
        Mensagem = e.Mensagem,
        DataAlteracao = e.DataAlteracao,
        DataCriacao = e.DataCriacao,
        Ativo = e.Ativo
    };
}
