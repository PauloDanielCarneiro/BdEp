using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InfectedBackEnd.Domain;
using Npgsql;

namespace InfectedBackEnd.Infrastructure.Database
{
    public class UserLocationRepository : IUserLocationRepository
    {
        private readonly NpgsqlConnection connection;

        public UserLocationRepository()
        {
            connection =
                new NpgsqlConnection(
                    "User ID=root;Password=rootroot;Host=database-1.cd4wnujqkcc1.us-east-2.rds.amazonaws.com;Port=5432;Database=infectedbackenddb;");
        }

        public async Task<IList<UserLocation>> GetAllUserLocationByUserId(Guid userId)
        {
            try
            {
                var sql =
                    @"select * from user_location where user_id = @userId and dateandtime >= CURRENT_DATE";

                var result = await this.connection.QueryAsync<UserLocation>(sql,
                    new
                    {
                        userId = userId
                    });

                return result.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<UserLocation>();
            }
        }

        public async Task<bool> AddUserLocation(UserLocation userLocation)
        {
            try
            {
                var sql =
                    $"Insert into user_location(id, dateandtime, location_id, user_id) values(@id, @dateandtime, @locationId, @userId)";

                var rowsAffected = await connection.ExecuteAsync(sql,
                    new
                    {
                        id = userLocation.Id,
                        dateandtime = userLocation.DateAndTime,
                        userId = userLocation.User_Id,
                        locationId = userLocation.Location_Id
                    });

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
