using System;

namespace InfectedBackEnd.Application.UseCases.User.DTO.Request
{
    public class PatchDiseaseToUserDto
    {
        public DateTime? EndDate { get; set; }
        public bool? Cured { get; set; }
        public bool? ShowSymptoms { get; set; }
    }
}
