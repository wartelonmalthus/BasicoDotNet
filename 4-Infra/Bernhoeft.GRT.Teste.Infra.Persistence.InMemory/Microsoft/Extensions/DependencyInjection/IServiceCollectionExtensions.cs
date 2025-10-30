﻿using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Infra.Persistence.SqlServer.ContractStore.Mappings;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Helper;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adicionar Context de Conexão com Banco de Dados.
        /// </summary>
        public static IServiceCollection AddDbContext(this IServiceCollection @this)
        {
            @this.AddBernhoeftDbContext<AvisoMap>((serviceProvider, options) =>
            {
                options.UseInMemoryDatabase("TesteDb")
                       .UseAsyncSeeding(async (context, _, cancellationToken) =>
                       {
                           var dbSet = context.Set<AvisoEntity>();
                           if (!await dbSet.AnyAsync(cancellationToken))
                           {
                               dbSet.Add(new AvisoEntity("Titulo 1", "Mensagem 1"));
                               dbSet.Add(new AvisoEntity("Titulo 2", "Mensagem 2"));
                               dbSet.Add(new AvisoEntity("Titulo 3", "Mensagem 3"));

                               await context.SaveChangesAsync(cancellationToken);
                           }
                       });
            });
            @this.RegisterServicesFromAssemblyContaining<AvisoMap>(); // Register Repositories with InjectServiceAttribute.

            // Create DataBase in Memory.
            using var serviceProvider = @this.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<IContext>();
            AsyncHelper.RunSync(() => ((DbContext)dbContext).Database.EnsureCreatedAsync());

            return @this;
        }
    }
}