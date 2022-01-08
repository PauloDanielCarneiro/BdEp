using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfectedBackEnd.Domain;

namespace InfectedBackEnd.Infrastructure.Database
{
    public interface IUserDiseaseRepository
    {
        Task<IList<UserDisease>> GetAllUserDiseases(Guid userId);
        Task<bool> AddUserDisease(UserDisease userDisease);
        Task<UserDisease> GetUserDiseaseById(Guid id);
        Task<UserDisease> UpdateUserDisease(UserDisease userDisease);
    }
}
