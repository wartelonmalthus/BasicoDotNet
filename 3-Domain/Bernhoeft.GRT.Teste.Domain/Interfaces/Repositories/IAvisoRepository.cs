using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Teste.Domain.Models.Aviso;
using Bernhoeft.GRT.Teste.Domain.Models.Common;


namespace Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories
{
    public interface IAvisoRepository : IRepository<AvisoEntity>
    {
        Task<List<AvisoEntity>> ObterTodosAvisosAsync(TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<PagedResult<AvisoEntity>> ObterAvisosComFiltrosAsync(AvisoFilter request, CancellationToken cancellationToken = default);
    }
}