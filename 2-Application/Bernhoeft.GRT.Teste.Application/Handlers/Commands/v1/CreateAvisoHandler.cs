using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;

public class CreateAvisoHandler(IAvisoRepository avisoRepository) : IRequestHandler<CreateAvisoRequest, IOperationResult<CreateAvisoResponse>>
{
    private readonly IAvisoRepository _avisoRepository = avisoRepository;

    public async Task<IOperationResult<CreateAvisoResponse>> Handle(CreateAvisoRequest request, CancellationToken cancellationToken)
    {
        var entity = new AvisoEntity(request.Titulo.Trim(), request.Mensagem.Trim());

        await _avisoRepository.AddAsync(entity, cancellationToken);
        await _avisoRepository.SaveChangesAsync(cancellationToken);

        var response = (CreateAvisoResponse)entity;

        return OperationResult<CreateAvisoResponse>.ReturnCreated();
    }
}
