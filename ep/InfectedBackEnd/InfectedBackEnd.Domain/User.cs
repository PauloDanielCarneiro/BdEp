using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfectedBackEnd.Domain
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string password, string email, string document)
        {
            Id = Guid.NewGuid();
            Name = name;
            Password = password;
            Email = email;
            Document = document;
            Token = Id;
        }

        [Column] public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public Guid Token { get; set; }
    }
}