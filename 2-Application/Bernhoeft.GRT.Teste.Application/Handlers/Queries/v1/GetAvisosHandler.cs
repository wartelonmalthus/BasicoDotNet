using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Teste.Domain.Models.Aviso;
using Bernhoeft.GRT.Teste.Domain.Models.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisosHandler(IServiceProvider serviceProvider) : IRequestHandler<GetAvisosRequest, IOperationResult<PagedResult<GetAvisosResponse>>>
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public async Task<IOperationResult<PagedResult<GetAvisosResponse>>> Handle(GetAvisosRequest request, CancellationToken cancellationToken)
        {
            PagedResult<AvisoEntity> result = await _avisoRepository.ObterAvisosComFiltrosAsync((AvisoFilter)request, cancellationToken);

            if (result.TotalRecords == 0)
                return OperationResult<PagedResult<GetAvisosResponse>>.ReturnNoContent();

            var pagedResponse = new PagedResult<GetAvisosResponse>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords,
                TotalPages = result.TotalPages,
                Data = result.Data.Select(x => (GetAvisosResponse)x).ToList()
            };

            return OperationResult<PagedResult<GetAvisosResponse>>.ReturnOk(pagedResponse);
        }
    }
    
}