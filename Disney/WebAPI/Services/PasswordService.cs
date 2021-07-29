using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Options;

namespace WebAPI.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions opcioncontrasena;
        public PasswordService(IOptions<PasswordOptions> options)
        {
            opcioncontrasena = options.Value;
        }
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);
            if(parts.Length != 3)
            {
                throw new Exception("Unexpected hash format");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
               password, salt, iterations
               ))
            {
                var keyToCheck = algorithm.GetBytes(opcioncontrasena.KeySize);
                return keyToCheck.SequenceEqual(key);
       
            }
        }

        public string Hash(string password)
        {
            //Implementación PBKDF2
            using (var algorithm = new Rfc2898DeriveBytes(
                password, opcioncontrasena.SaltSize, opcioncontrasena.Iterations
                ))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(opcioncontrasena.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{opcioncontrasena.Iterations}.{salt}.{key}";
            }

        }
    }
}
