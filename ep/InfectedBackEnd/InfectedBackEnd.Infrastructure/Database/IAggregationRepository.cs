using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfectedBackEnd.Domain;

namespace InfectedBackEnd.Infrastructure.Database
{
    public interface IAggregationRepository
    {
        Task<IList<Aggregation>> GetRelevantData(Guid userId, DateTime? startDate = null, DateTime? endDate = null);
    }
}
