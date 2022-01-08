using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfectedBackEnd.Domain;

namespace InfectedBackEnd.Infrastructure.Database
{
    public interface IUserLocationRepository
    {
        Task<bool> AddUserLocation(UserLocation userLocation);
        Task<IList<UserLocation>> GetAllUserLocationByUserId(Guid userId);
    }
}
