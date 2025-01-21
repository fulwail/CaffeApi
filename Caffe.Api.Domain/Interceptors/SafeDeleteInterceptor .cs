using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caffe.Api.Domain.Models;

namespace Caffe.Api.Domain.Interceptors
{
    public class SafeDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null) return new ValueTask<InterceptionResult<int>>(result);

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: SafeDeletableEntity delete }) continue;
                entry.State = EntityState.Modified;
                delete.IsDeleted = true;
            }
            return new ValueTask<InterceptionResult<int>>(result);
        }
    }
}
