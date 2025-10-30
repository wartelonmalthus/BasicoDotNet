using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;

public class DeleteAvisoHandler(IAvisoRepository avisoRepository) : IRequestHandler<DeleteAvisoRequest, IOperationResult<DeleteAvisoResponse>>
{
    private readonly IAvisoRepository _avisoRepository = avisoRepository;

    public async Task<IOperationResult<DeleteAvisoResponse>> Handle(DeleteAvisoRequest request, CancellationToken cancellationToken)
    {
        var aviso = await _avisoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (aviso is null)
            return OperationResult<DeleteAvisoResponse>.ReturnNotFound()
                .AddMessage("Aviso não encontrado.");

        aviso.DelecaoLogica();

         _avisoRepository.Update(aviso);
        await _avisoRepository.SaveChangesAsync(cancellationToken);

        return OperationResult<DeleteAvisoResponse>.ReturnOk(new DeleteAvisoResponse{ Id = aviso.Id });
    }
}
