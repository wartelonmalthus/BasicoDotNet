using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;

public class UpdateAvisoHandler(IAvisoRepository avisoRepository) : IRequestHandler<UpdateAvisoRequest, IOperationResult<UpdateAvisoResponse>>
{
    private readonly IAvisoRepository _avisoRepository = avisoRepository;

    public async Task<IOperationResult<UpdateAvisoResponse>> Handle(UpdateAvisoRequest request, CancellationToken cancellationToken)
    {

        var aviso = await _avisoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (aviso is null)
            return OperationResult<UpdateAvisoResponse>.ReturnNotFound()
                .AddMessage("Aviso não encontrado.");

        aviso.Atualizar(request.Titulo, request.Mensagem);
      
         _avisoRepository.Update(aviso);
        await _avisoRepository.SaveChangesAsync(cancellationToken);

        return OperationResult<UpdateAvisoResponse>.ReturnOk((UpdateAvisoResponse)aviso);
    }
}

