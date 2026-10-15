using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace MIPHazi
{
    public class Director : Employee, Authenticatable
    {
        private static Director instance = null;

        private string passwordHash;
        private string timestamp;

        private Director(string name, double salary, string rawPassword) 
            : base(name, salary)
        {
            passwordHash = Security.HashPassword(rawPassword);
            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static Director GetInstance(string name, double salary, string rawPassword)
        {
            if (instance == null)
            {
                instance = new Director(name, salary, rawPassword);
            }
            return instance;
        }

        public void PrintTotalSalaries()
        {
            Console.WriteLine("Összes fizetés: " + Employee.GetTotalSalaries());
        }

        public string GetPasswordHash()
        {
            return passwordHash;
        }
        public string GetTimestamp()
        {
            return timestamp;
        }

        public bool CheckPassword(string rawPassword)
        {
            return Security.VerifyPassword(rawPassword, passwordHash);
        }
    }
}
