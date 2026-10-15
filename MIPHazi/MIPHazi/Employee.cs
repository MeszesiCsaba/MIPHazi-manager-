using System;
using System.Collections.Generic;
using System.Text;

namespace MIPHazi
{
    public class Employee : Person
    {
        private double salary;
        private static double totalSalaries = 0;
        private static object salaryLock = new object();
        private Manager manager;
        public Employee(string name, double salary) : base(name)
        {
            SetSalary(salary);
        }
        public bool HasManager()
        {
            return manager != null;
        }
        public Manager GetManager()
        {
            return manager;
        }
        public void SetManager(Manager manager)
        {
            if (HasManager())
            {
                throw new Exception("Employee already has a manager.");
            }
            else
            {
                this.manager = manager;
            }
        }
        public static double GetTotalSalaries()
        {
            return totalSalaries;
        }
        public double GetSalary()
        {
            return salary;
        }
        public void SetSalary(double salary)
        {
            if (salary <= 0)
            {
                throw new ArgumentException("Salary must be a positive number.");
            }
            this.salary = salary;
            lock (salaryLock)
            {
                totalSalaries += salary;
            }
        }

    }
}
