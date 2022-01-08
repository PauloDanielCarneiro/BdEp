using System;

namespace InfectedBackEnd.Application.UseCases.User.DTO.Response
{
    public class AddDiseaseToUserResponseDto
    {
        public AddDiseaseToUserResponseDto(Guid userDiseaseId, Guid userId, Guid diseaseId, bool cured)
        {
            UserDiseaseId = userDiseaseId;
            UserId = userId;
            DiseaseId = diseaseId;
            Cured = cured;
        }

        public AddDiseaseToUserResponseDto()
        {
        }

        public Guid UserDiseaseId { get; set; }
        public Guid UserId { get; set; }
        public Guid DiseaseId { get; set; }
        public bool? Cured { get; set; }
    }
}