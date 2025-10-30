using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1;

public class GetAvisoByIdHandler(IAvisoRepository avisoRepository) : IRequestHandler<GetAvisoByIdRequest, IOperationResult<GetAvisoByIdResponse>>
{
    private readonly IAvisoRepository _avisoRepository = avisoRepository;

    public async Task<IOperationResult<GetAvisoByIdResponse>> Handle(GetAvisoByIdRequest request, CancellationToken cancellationToken)
    {
        var aviso = await _avisoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (aviso is null)
            return OperationResult<GetAvisoByIdResponse>.ReturnNotFound()
                .AddMessage("Aviso não encontrado.");

        return OperationResult<GetAvisoByIdResponse>.ReturnOk((GetAvisoByIdResponse)aviso);
    }
}

