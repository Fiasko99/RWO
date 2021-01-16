using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWO
{
    class ValidateUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecretCode { get; set; }


        public ValidateUser(
                string login,
                string password,
                string role
            )
        {
            Login = login;
            Password = password;
            Role = role;
        }
        public ValidateUser(
                string login,
                string password,
                string role,
                string email,
                string name,
                string surname,
                string secretcode
            )
        {
            Login = login;
            Password = password;
            Role = role;
            Email = email;
            Surname = name;
            Name = surname;
            SecretCode = secretcode;
        }
    }
}
