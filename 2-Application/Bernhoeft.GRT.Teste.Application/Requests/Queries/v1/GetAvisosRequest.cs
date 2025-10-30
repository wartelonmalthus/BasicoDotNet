using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Teste.Domain.Models.Aviso;
using Bernhoeft.GRT.Teste.Domain.Models.Common;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;

public class GetAvisosRequest : PaginacaoOrdenacao, IRequest<IOperationResult<PagedResult<GetAvisosResponse>>>
{
    public string? Titulo { get; set; }
    public string? Mensagem { get; set; }
    public bool? Ativo { get; set; }
    public DateTime? DataCriacaoInicio { get; set; }
    public DateTime? DataCriacaoFim { get; set; }

    public static implicit operator AvisoFilter(GetAvisosRequest e) => new()
    {
        Titulo = e.Titulo,
        Mensagem = e.Mensagem,
        DataCriacaoFim = e.DataCriacaoFim,
        DataCriacaoInicio = e.DataCriacaoInicio,
        Descending = e.Descending,
        OrderBy = e.OrderBy,
        PageNumber = e.PageNumber,
        PageSize = e.PageSize,
        Ativo = e.Ativo
    };
}