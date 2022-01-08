using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InfectedBackEnd.Domain;
using Npgsql;

namespace InfectedBackEnd.Infrastructure.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlConnection connection;

        public UserRepository()
        {
            connection =
                new NpgsqlConnection(
                    "User ID=root;Password=rootroot;Host=database-1.cd4wnujqkcc1.us-east-2.rds.amazonaws.com;Port=5432;Database=infectedbackenddb;");
        }

        public async Task<User> GetUser(string document)
        {
            var userDocument = new string(document.Where(x => char.IsDigit(x)).ToArray());
            var user = await connection.QueryFirstOrDefaultAsync<User>("Select * from users where document = @document",
                new {document = userDocument});
            await connection.CloseAsync();
            return user;
        }

        public async Task<User> GetUser(Guid token)
        {
            try
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>("Select * from users where token = @token",
                    new {token = token});
                await connection.CloseAsync();
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Guid> CreateUser(User user)
        {
            var sql =
                "Insert into users(id, name, password, email, token, document) values (@id, @name, @password, @email, @token, @document)";
            try
            {
                var rowsAffected = await connection.ExecuteAsync(sql,
                    new
                    {
                        id = user.Id, name = user.Name, password = user.Password, email = user.Email,
                        token = user.Token, document = user.Document
                    });

                return user.Id;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Guid.Empty;
            }
        }

        // public async Task<User> UpdateUser(User user)
        // {
        //     var sql = "Update users set id = @id, name = @name, email = @email, phone = @phone, phoneidentificator = @phIdentificator Where id = @userId";
        //     var rowsAffected = connection.ExecuteAsync(sql, new { id = user.Id, name = user.Name, email = user.Email, phone = user.Phone, phIdentificator = user.PhoneIdentificator, userId = user.Id });
        //     return user;
        // }

        /// <summary>
        /// Log in the user. We are not worried about the security in this first version, so we do not implement a fancy way to handle authentication information
        /// </summary>
        /// <param name="document">Document to use in log in</param>
        /// <param name="password">User password</param>
        /// <returns></returns>
        public async Task<bool> Login(string document, string password)
        {
            var user = await GetUser(document);
            return user.Password == password;
        }
    }
}