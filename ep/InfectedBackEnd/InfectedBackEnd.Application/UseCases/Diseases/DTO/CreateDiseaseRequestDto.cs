using System;

namespace InfectedBackEnd.Application.UseCases.Diseases.DTO
{
    public class CreateDiseaseRequestDto
    {
        public string Name { get; set; }
        public bool Contagious { get; set; }
    }
}