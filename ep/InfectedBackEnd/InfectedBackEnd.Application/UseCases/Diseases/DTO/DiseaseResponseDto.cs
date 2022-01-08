using System;

namespace InfectedBackEnd.Application.UseCases.Diseases.DTO
{
    public class DiseaseResponseDto
    {
        public DiseaseResponseDto(Guid id, string name, bool contagious)
        {
            Id = id;
            Name = name;
            Contagious = contagious;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Contagious { get; set; }
    }
}