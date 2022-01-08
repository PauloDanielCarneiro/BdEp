using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InfectedBackEnd.Domain;
using Npgsql;

namespace InfectedBackEnd.Infrastructure.Database
{
    public class DiseasesRepository : IDiseasesRepository
    {
        private readonly NpgsqlConnection connection;

        public DiseasesRepository()
        {
            connection =
                new NpgsqlConnection(
                    "User ID=root;Password=rootroot;Host=database-1.cd4wnujqkcc1.us-east-2.rds.amazonaws.com;Port=5432;Database=infectedbackenddb;");
        }

        public async Task<Disease> GetDiseaseByName(string name)
        {
            try
            {
                var disease = await connection.QueryFirstOrDefaultAsync<Disease>(
                    $"Select * from diseases where name = @name",
                    new {name = name});
                return disease;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Disease> GetDiseaseById(string id)
        {
            var disease = await connection.QueryFirstOrDefaultAsync(
                $"Select * from diseases where id = @id",
                new {id = id});
            return disease;
        }

        public async Task<IList<Disease>> GetAllDiseases()
        {
            var diseaseList = await connection.QueryAsync<Disease>(
                $"Select * from diseases");

            return diseaseList.ToList();
        }

        public async Task<Guid> CreateDisease(Disease disease)
        {
            try
            {
                var sql = $"Insert into diseases(id, name, contagious) values (@id, @name, @contagious)";
                var rowsAffected = await connection.ExecuteAsync(sql,
                    new {id = disease.Id, name = disease.Name, contagious = disease.Contagious});
                return disease.Id;
            }
            catch (PostgresException _)
            {
                throw new Exception("A doença já está cadastrada.");
            }
        }
    }
}
