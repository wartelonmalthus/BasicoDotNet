﻿using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Extensions;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisosHandler : IRequestHandler<GetAvisosRequest, IOperationResult<IEnumerable<GetAvisosResponse>>>
    {
        private readonly IServiceProvider _serviceProvider;

        private IContext _context => _serviceProvider.GetRequiredService<IContext>();
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public GetAvisosHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<IEnumerable<GetAvisosResponse>>> Handle(GetAvisosRequest request, CancellationToken cancellationToken)
        {
            List<AvisoEntity> result = await _avisoRepository.ObterTodosAvisosAsync(TrackingBehavior.NoTracking);
            if (!result.HaveAny())
                return OperationResult<IEnumerable<GetAvisosResponse>>.ReturnNoContent();

            return OperationResult<IEnumerable<GetAvisosResponse>>.ReturnOk(result.Select(x => (GetAvisosResponse)x));
        }
    }
}