using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;

public record class GetAvisoByIdRequest(int id) : IRequest<IOperationResult<GetAvisoByIdResponse>>;



