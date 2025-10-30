﻿using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;

public class DeleteAvisoRequest : IRequest<IOperationResult<DeleteAvisoResponse>>
{
    public int Id { get; set; }
}

