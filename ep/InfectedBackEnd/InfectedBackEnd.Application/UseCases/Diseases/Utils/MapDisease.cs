using System;
using InfectedBackEnd.Application.UseCases.Diseases.DTO;

namespace InfectedBackEnd.Application.UseCases.Diseases.Utils
{
    public static class MapDisease
    {
        public static DiseaseResponseDto ToCreateResponse(this Domain.Disease disease)
        {
            return new(disease.Id, disease.Name, disease.Contagious);
        }

        public static Domain.Disease ToDiseaseDomain(this CreateDiseaseRequestDto diseaseRequestDto)
        {
            return new(Guid.NewGuid(), diseaseRequestDto.Name, diseaseRequestDto.Contagious);
        }
    }
}