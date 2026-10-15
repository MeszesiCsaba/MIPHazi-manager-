using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MIPHazi
{
    public class DataReader
    {
        public static List<Employee> ReadEmployees(string filename)
        {
            List<Employee> employees = new List<Employee>();

            string json = File.ReadAllText(filename);

            CompanyData data =
                JsonSerializer.Deserialize<CompanyData>(json);

            foreach (EmployeeData item in data.employees)
            {
                try
                {
                    Employee employee = new Employee(
                        item.name,
                        item.salary
                    );

                    employees.Add(employee);
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Hibas Employee: " + e.Message
                    );
                }
            }

            return employees;
        }


        public static List<Manager> ReadManagers(string filename)
        {
            List<Manager> managers = new List<Manager>();

            string json = File.ReadAllText(filename);

            CompanyData data =
                JsonSerializer.Deserialize<CompanyData>(json);

            foreach (ManagerData item in data.managers)
            {
                try
                {
                    if (item.password == null)
                    {
                        throw new Exception(
                            "A manager jelszava hianyzik."
                        );
                    }

                    Manager manager = new Manager(
                        item.name,
                        item.salary,
                        item.password
                    );

                    managers.Add(manager);
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Hibas Manager: " + e.Message
                    );
                }
            }

            return managers;
        }


        public class CompanyData
        {
            public List<EmployeeData> employees { get; set; }
            public List<ManagerData> managers { get; set; }
        }


        public class EmployeeData
        {
            public string name { get; set; }
            public double salary { get; set; }
        }


        public class ManagerData
        {
            public string name { get; set; }
            public double salary { get; set; }
            public string password { get; set; }
        }
    }
}