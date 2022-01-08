using System;
using System.Threading.Tasks;
using InfectedBackEnd.Domain;

namespace InfectedBackEnd.Infrastructure.Database
{
    public interface IUserRepository
    {
        Task<Guid> CreateUser(User user);
        Task<User> GetUser(Guid token);
        Task<User> GetUser(string document);
        Task<bool> Login(string document, string password);
    }
}