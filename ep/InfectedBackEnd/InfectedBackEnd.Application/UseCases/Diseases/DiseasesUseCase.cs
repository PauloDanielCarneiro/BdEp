using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfectedBackEnd.Application.UseCases.Diseases.DTO;
using InfectedBackEnd.Application.UseCases.Diseases.Utils;
using InfectedBackEnd.Application.UseCases.User;
using InfectedBackEnd.Domain;
using InfectedBackEnd.Infrastructure.Database;

namespace InfectedBackEnd.Application.UseCases.Diseases
{
    public interface IDiseasesUseCase
    {
        Task<IList<DiseaseResponseDto>> GetAllDiseases(Guid token);
        Task<DiseaseResponseDto> CreateDisease(CreateDiseaseRequestDto request, Guid token);
        Task<DiseaseResponseDto> GetDiseaseByName(Guid token, string name);
    }

    public class DiseasesUseCase : IDiseasesUseCase
    {
        private readonly IDiseasesRepository diseaseRepository;
        private readonly IUserUseCase userUseCase;

        public DiseasesUseCase(IDiseasesRepository diseaseRepository, IUserUseCase userUseCase)
        {
            this.diseaseRepository = diseaseRepository;
            this.userUseCase = userUseCase;
        }

        public async Task<DiseaseResponseDto> GetDiseaseByName(Guid token, string name)
        {
            var user = await userUseCase.GetUserByToken(token);
            if (user is null) return null;

            var disease = await this.diseaseRepository.GetDiseaseByName(name);

            return disease.ToCreateResponse();
        }

        public async Task<IList<DiseaseResponseDto>> GetAllDiseases(Guid token)
        {
            var user = await userUseCase.GetUserByToken(token);
            if (user is null) return null;

            var diseases = await diseaseRepository.GetAllDiseases();

            return diseases.Select(x => x.ToCreateResponse()).ToList();
        }

        public async Task<DiseaseResponseDto> CreateDisease(CreateDiseaseRequestDto request, Guid token)
        {
            var user = await userUseCase.GetUserByToken(token);
            if (user is null) return null;

            var disease = request.ToDiseaseDomain();
            var _ = await diseaseRepository.CreateDisease(disease);
            return disease.ToCreateResponse();
        }
    }
}
