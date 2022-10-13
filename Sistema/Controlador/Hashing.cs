using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Controlador
{
    public class Hashing
    {
        public string ToSHA256(string s)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    

    public string Descrypt(string s)
    {
        Usuario us = new Usuario();

        string hash = us.contrasenna;
        byte[] data = Convert.FromBase64String(s);
        SHA256 sha256 = SHA256.Create();

        TripleDES tripledes = TripleDES.Create();
        tripledes.Key = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
        tripledes.Mode = CipherMode.CBC;
        tripledes.Padding = PaddingMode.PKCS7;

        ICryptoTransform transform = tripledes.CreateDecryptor();
        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);

        return UTF8Encoding.UTF8.GetString(results);

    }
    }
}
