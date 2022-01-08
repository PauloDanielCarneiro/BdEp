using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using InfectedBackEnd.Domain;
using Npgsql;

namespace InfectedBackEnd.Infrastructure.Database
{
    public class LocationRepository : ILocationRepository
    {
        private readonly NpgsqlConnection connection;

        public LocationRepository()
        {
            connection =
                new NpgsqlConnection(
                    "User ID=root;Password=rootroot;Host=database-1.cd4wnujqkcc1.us-east-2.rds.amazonaws.com;Port=5432;Database=infectedbackenddb;");
        }

        public async Task<List<Location>> GetAllLocations()
        {
            var locations = await connection.QueryAsync<Location>("Select * from locations");
            await connection.CloseAsync();
            return locations.ToList();
        }

        public async Task<IList<Location>> GetAllLocationByIds(IList<Guid> ids)
        {
            try
            {
                var sql = @"
                    SELECT *
                    from locations
                    where id = ANY(@ids);
                ";

                var result = await this.connection.QueryAsync<Location>(sql,
                    new
                    {
                        ids = ids.ToArray()
                    });

                return result.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Location>();
            }
        }

        // public async Task<IList<Location>> GetCloseLocationFromCoordenates(IList<Location> startLocations)
        // {
        //     try
        //     {
        //         List<Location> resultLocations = new();
        //         foreach (var location in startLocations)
        //         {
        //             var sql =
        //         }
        //
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         throw;
        //     }
        // }

        public async Task<Location> GetLocationByCoordinates(double lat, double lg)
        {
            var sql = $"Select * from locations where st_dwithin(geom, st_makepoint(@lat, @lg)::geography, 10)";
            var location = await connection.QueryFirstOrDefaultAsync<Location>(
                sql,
                new {lat = lat, lg = lg});

            return location;
        }

        // public async Task<Location> GetLocationByCoordinates(double lat, double lg)
        // {
        //     var location = await connection.QueryFirstOrDefaultAsync<Location>(
        //         "Select * from locations where latitude= @lat and longitude = @lg",
        //         new {lat = lat, lg = lg});
        //
        //     return location;
        // }


        public async Task<Guid> CreateLocation(Location location)
        {
            try
            {
                var sql = @"Insert into locations(Id, Latitude, Longitude, name) values (@id, @lat, @longitude, @name)";
                var _ = await connection.ExecuteAsync(sql,
                    new
                    {
                        id = location.Id, lat = location.Latitude, longitude = location.Longitude, name = location.Name
                    });

                var sqlUpdateLocation =
                    $"update locations set geom = st_setsrid(st_makepoint(@longitude, @latitude), 4326) where id = @id";
                _ = await connection.ExecuteAsync(sqlUpdateLocation,
                    new
                    {
                        longitude = location.Latitude,
                        latitude = location.Longitude,
                        id = location.Id
                    });
                return location.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Guid.Empty;
            }
        }
    }
}
