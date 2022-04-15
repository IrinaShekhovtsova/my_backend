using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Shekhovtsova_backend.Models
{

    public class Person
    {
        public int PersonID { get; set; }
        public string Login { get; set; }

        public string Role { get; set; }

        private byte[] password;
        public string Password
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var b in new MD5CryptoServiceProvider().ComputeHash(password))
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
            set { password = Encoding.UTF8.GetBytes(value); }
        }



    }
}
