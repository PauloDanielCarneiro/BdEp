using System;

namespace InfectedBackEnd.Application.UseCases.User.DTO.Request
{
    public class AddDiseaseToUserDto
    {
        public bool Cured { get; set; }
        public bool ShowSymptoms { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DiseaseName { get; set; }
    }
}
