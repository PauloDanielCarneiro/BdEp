using System;

namespace InfectedBackEnd.Domain
{
    public class UserLocation
    {
        public UserLocation()
        {

        }
        public UserLocation(Guid userId, Guid locationId, DateTime dateAndTime, Guid? id)
        {
            User_Id = userId;
            Location_Id = locationId;
            DateAndTime = dateAndTime;
            Id = id ?? Guid.NewGuid();
        }

        public Guid User_Id { get; set; }
        public Guid Location_Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public Guid Id { get; set; }
    }
}
