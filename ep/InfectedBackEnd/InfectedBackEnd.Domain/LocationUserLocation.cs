using System;

namespace InfectedBackEnd.Domain
{
    public class LocationUserLocation
    {
        public LocationUserLocation(Guid id, double latitude, double longitude, string name, Guid userId, Guid locationId, DateTime dateAndTime)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
            Name = name;
            User_Id = userId;
            Location_Id = locationId;
            DateAndTime = dateAndTime;
        }

        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public Guid User_Id { get; set; }
        public Guid Location_Id { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}
