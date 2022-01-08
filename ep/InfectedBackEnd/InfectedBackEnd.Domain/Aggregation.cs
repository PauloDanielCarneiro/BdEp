using System;

namespace InfectedBackEnd.Domain
{
    public class Aggregation
    {
        public Guid UserId { get; set; }
        public Guid LocationId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DateReturn { get; set; }
        public bool Cured { get; set; }

        public Aggregation()
        {
        }

        public Aggregation(Guid userId, Guid locationId, double longitude,
            double latitude, DateTime dateReturn, bool cured)
        {
            this.UserId = userId;
            this.LocationId = locationId;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.DateReturn = dateReturn;
            this.Cured = cured;
        }
    }
}
