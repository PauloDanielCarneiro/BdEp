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
    public class UserDiseaseRepository : IUserDiseaseRepository
    {
        private readonly NpgsqlConnection connection;

        public UserDiseaseRepository()
        {
            connection =
                new NpgsqlConnection(
                    "User ID=root;Password=rootroot;Host=database-1.cd4wnujqkcc1.us-east-2.rds.amazonaws.com;Port=5432;Database=infectedbackenddb;");
        }

        public async Task<IList<UserDisease>> GetAllUserDiseases(Guid userId)
        {
            try
            {
                var sql =
                    $"Select id, cured, show_symptoms, startdate, enddate, user_id, disease_id from user_diseases where user_id = @userId";

                var diseases = await connection.QueryAsync<UserDisease>(sql,
                    new
                    {
                        userId = userId
                    });

                return diseases.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<UserDisease> GetUserDiseaseById(Guid id)
        {
            try
            {
                var sql = "Select * from user_diseases where id = @id";
                var result = await connection.QueryFirstOrDefaultAsync<UserDisease>(sql,
                    new {id = id});
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new UserDisease();
            }
        }

        public async Task<bool> AddUserDisease(UserDisease userDisease)
        {
            try
            {
                var sql =
                    $"Insert into user_diseases(id, cured, show_symptoms, startdate, enddate, user_id, disease_id) values(@id, @cured, @show_symptoms, @startdate, @enddate, @user_id, @disease_id)";

                var rowsAffected = await connection.ExecuteAsync(sql,
                    new
                    {
                        Id = userDisease.Id,
                        cured = userDisease.Cured,
                        show_symptoms = userDisease.Show_Symptoms,
                        startdate = userDisease.StartDate,
                        enddate = userDisease.EndDate,
                        user_id = userDisease.User_Id,
                        disease_id = userDisease.Disease_Id
                    });

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<UserDisease> UpdateUserDisease(UserDisease userDisease)
        {
            try
            {
                var sql =
                    $"update User_diseases set cured = @cured, show_symptoms = @showSymptoms, enddate = @endDate where id = @id";

                var rowsAffected = await connection.ExecuteAsync(sql,
                    new
                    {
                        cured = userDisease.Cured,
                        showSymptoms = userDisease.Show_Symptoms,
                        endDate = userDisease.EndDate,
                        id = userDisease.Id
                    });

                return userDisease;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new UserDisease();
            }
        }
    }
}
