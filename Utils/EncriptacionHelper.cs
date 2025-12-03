using System;
using System.Security.Cryptography;
using System.Text;

namespace Desafio1App.Utils
{
    public static class EncriptacionHelper
    {
        public static string EncriptarContrasena(string contrasena)
        {
            if (string.IsNullOrEmpty(contrasena))
                return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(contrasena);
                byte[] hash = sha256.ComputeHash(bytes);
                
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerificarContrasena(string contrasena, string hash)
        {
            string hashContrasena = EncriptarContrasena(contrasena);
            return string.Equals(hashContrasena, hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}

