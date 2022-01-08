using System;

namespace InfectedBackEnd.Domain
{
    public class Location
    {
        public Location(Guid? id, double latitude, double lg)
        {
            Id = id ?? Guid.NewGuid();
            Latitude = latitude;
            Longitude = lg;
            Name = "To Implement with google api";
        }

        public Location()
        {
        }

        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
    }
}