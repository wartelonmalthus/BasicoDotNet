using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Attributes;
using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Teste.Domain.Models.Aviso;
using Bernhoeft.GRT.Teste.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.ContractWeb.Infra.Persistence.SqlServer.ContractStore.Repositories
{
    [InjectService(Interface: typeof(IAvisoRepository))]
    public class AvisoRepository : Repository<AvisoEntity>, IAvisoRepository
    {
        public AvisoRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<List<AvisoEntity>> ObterTodosAvisosAsync(TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default)
        {
            var query = tracking is TrackingBehavior.NoTracking ? Set.AsNoTrackingWithIdentityResolution() : Set;
            return query.ToListAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedResult<AvisoEntity>> ObterAvisosComFiltrosAsync(AvisoFilter request, CancellationToken cancellationToken = default)
        {
            var query = Set.AsNoTrackingWithIdentityResolution().AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Titulo))
                query = query.Where(x => x.Titulo.Contains(request.Titulo));

            if (!string.IsNullOrWhiteSpace(request.Mensagem))
                query = query.Where(x => x.Mensagem.Contains(request.Mensagem));

            if (request.Ativo.HasValue)
                query = query.Where(x => x.Ativo == request.Ativo.Value);

            if (request.DataCriacaoInicio.HasValue)
                query = query.Where(x => x.DataCriacao >= request.DataCriacaoInicio.Value);

            if (request.DataCriacaoFim.HasValue)
                query = query.Where(x => x.DataCriacao <= request.DataCriacaoFim.Value);

            query = request.OrderBy?.ToLower() switch
            {
                "titulo" => request.Descending ? query.OrderByDescending(x => x.Titulo) : query.OrderBy(x => x.Titulo),
                "id" => request.Descending ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id),
                "datacriacao" or _ => request.Descending ? query.OrderByDescending(x => x.DataCriacao) : query.OrderBy(x => x.DataCriacao)
            };

            var totalRecords = await query.CountAsync(cancellationToken);

            var data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<AvisoEntity>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize),
                Data = data
            };
        }
    }
}