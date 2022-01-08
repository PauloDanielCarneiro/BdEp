using System;

namespace InfectedBackEnd.Domain
{
    public class UserDisease
    {
        public UserDisease()
        {
        }

        public UserDisease(Guid? id, bool cured, bool showSymptoms, DateTime startDate, DateTime endDate, Guid userId,
            Guid diseaseId)
        {
            Id = id ?? Guid.NewGuid();
            Cured = cured;
            Show_Symptoms = showSymptoms;
            StartDate = startDate;
            EndDate = endDate;
            User_Id = userId;
            Disease_Id = diseaseId;
        }

        public Guid Id { get; set; }
        public bool Cured { get; set; }
        public bool Show_Symptoms { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid User_Id { get; set; }
        public Guid Disease_Id { get; set; }
    }
}
