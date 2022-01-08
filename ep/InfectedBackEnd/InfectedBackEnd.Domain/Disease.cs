using System;

namespace InfectedBackEnd.Domain
{
    public class Disease
    {
        public Disease(Guid id, string name, bool contagious)
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