using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InfectedBackEnd.Domain;
using Npgsql;

namespace InfectedBackEnd.Infrastructure.Database
{
    public class AggregationRepository : IAggregationRepository
    {
        private readonly NpgsqlConnection connection;

        public AggregationRepository()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            connection =
                new NpgsqlConnection(
                    "User ID=root;Password=rootroot;Host=database-1.cd4wnujqkcc1.us-east-2.rds.amazonaws.com;Port=5432;Database=infectedbackenddb;");
        }

        public async Task<IList<Aggregation>> GetRelevantData(Guid userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            startDate = startDate ?? DateTime.Today;
            endDate = endDate ?? DateTime.Today;

            try
            {
                var sql = @"select * from Get_Relevant_Data(@userId, current_date - 1);";

                var result = await this.connection.QueryAsync<Aggregation>(sql,
                    new
                    {
                        userId = userId
                    });

                return result.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
