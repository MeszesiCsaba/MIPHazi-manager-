using System;
using System.Collections.Generic;
using System.Text;

namespace MIPHazi
{
    public class Manager : Employee, Authenticatable
    {
        private List<Employee> employees = new List<Employee>();
        private string passwordHash;
        private string timestamp;
        public Manager(string name, double salary, string rawPassword) : base(name, salary)
        {
            passwordHash = Security.HashPassword(rawPassword);
            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff");
        }
        public void AddEmployee(Employee employee)
        {
            if (employee.HasManager())
            {
                throw new Exception("Employee already has a manager.");
            }
            else
            {
                employee.SetManager(this);
                employees.Add(employee);
            }
        }
        public void PrintEmployees()
        {
            Console.WriteLine(GetName() + "beosztottjai:");

            if(employees.Count == 0)
            {
                Console.WriteLine("Nincs beosztott.");
            }
            else
            {
                foreach (Employee employee in employees)
                {
                    Console.WriteLine("- " + employee.GetName()+ " (" + employee.GetSalary() + ")");
                }
            }
        }
        public string GetPasswordHash()
        {
            return passwordHash;
        }

        public string GetTimestamp()
        {
            return timestamp;
        }

        public bool CheckPassword(string inputPassword)
        {
            return Security.VerifyPassword(inputPassword, passwordHash);
        }
    }
}
