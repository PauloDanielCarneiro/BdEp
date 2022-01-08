using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfectedBackEnd.Domain;

namespace InfectedBackEnd.Infrastructure.Database
{
    public interface IDiseasesRepository
    {
        Task<Disease> GetDiseaseByName(string name);
        Task<Disease> GetDiseaseById(string id);
        Task<IList<Disease>> GetAllDiseases();
        Task<Guid> CreateDisease(Disease disease);
    }
}