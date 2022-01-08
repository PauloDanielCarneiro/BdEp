using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfectedBackEnd.Domain;

namespace InfectedBackEnd.Infrastructure.Database
{
    public interface ILocationRepository
    {
        Task<Guid> CreateLocation(Location location);
        Task<Location> GetLocationByCoordinates(double lat, double lg);
        Task<List<Location>> GetAllLocations();
        Task<IList<Location>> GetAllLocationByIds(IList<Guid> ids);
    }
}
