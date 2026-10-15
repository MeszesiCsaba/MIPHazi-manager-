using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;

namespace MIPHazi
{
    internal class Program
    {

        static bool Login(Authenticatable user)
        {
            Console.Write(
                "Add meg a jelszot (" +
                user.GetName() +
                "): "
            );

            string password = Console.ReadLine();

            return user.CheckPassword(password);
        }
        static void Main(string[] args)
        {


            List<Employee> employees = null;
            List<Manager> managers = null;

            string filePath = Path.Combine(
                AppContext.BaseDirectory,
                "szemelyek.json"
            );


            Thread employeeThread = new Thread(() =>
            {
                employees = DataReader.ReadEmployees(filePath);
            });


            Thread managerThread = new Thread(() =>
            {
                managers = DataReader.ReadManagers(filePath);
            });


            Console.WriteLine("Adatok betoltese...");


            employeeThread.Start();
            managerThread.Start();


            employeeThread.Join();
            managerThread.Join();


            Console.WriteLine();
            Console.WriteLine("Betoltes befejezve.");

            Console.WriteLine(
                "Employee-k szama: " + employees.Count
            );

            Console.WriteLine(
                "Manager-ek szama: " + managers.Count
            );

            if (managers.Count >= 2 &&
                employees.Count >= 3)
            {
                managers[0].AddEmployee(employees[0]);
                managers[0].AddEmployee(employees[1]);

                managers[1].AddEmployee(employees[2]);
            }


            bool menu = true;


            while (menu)
            {
                Console.WriteLine();
                Console.WriteLine("------ Ceges Rendszer ------");
                Console.WriteLine("1. Bejelentkezes igazgatokent");
                Console.WriteLine("2. Bejelentkezes menedzserkent");
                Console.WriteLine("3. Employee-k listazasa");
                Console.WriteLine("4. Osszes fizetes");
                Console.WriteLine("5. Kilepes");
                Console.Write("Valassz egy opciot: ");


                int choice;

                if (!int.TryParse(
                    Console.ReadLine(),
                    out choice))
                {
                    Console.WriteLine(
                        "Kerlek szamot adj meg!"
                    );

                    continue;
                }


                switch (choice)
                {
                    case 1:

                        Director director =
                            Director.GetInstance(
                                "Nagy Bela",
                                10000,
                                "fonok123"
                            );


                        if (!Login(director))
                        {
                            Console.WriteLine("Invalid credentials!");
                            break;
                        }


                        Console.WriteLine();
                        Console.WriteLine(
                            "-------- Igazgatoi menu --------"
                        );


                        director.PrintTotalSalaries();

                        break;


                    case 2:

                        if (managers.Count == 0)
                        {
                            Console.WriteLine(
                                "Nincs manager."
                            );

                            break;
                        }


                        Console.WriteLine();
                        Console.WriteLine(
                            "Melyik menedzserkent szeretnel belepni?"
                        );


                        for (int i = 0;
                             i < managers.Count;
                             i++)
                        {
                            Console.WriteLine(
                                (i + 1) +
                                ". " +
                                managers[i].GetName()
                            );
                        }


                        Console.Write("Valassz: ");


                        int managerChoice;

                        if (!int.TryParse(
                            Console.ReadLine(),
                            out managerChoice))
                        {
                            Console.WriteLine(
                                "Ervenytelen valasz!"
                            );

                            break;
                        }


                        if (managerChoice < 1 ||
                            managerChoice > managers.Count)
                        {
                            Console.WriteLine(
                                "Ervenytelen valasz!"
                            );

                            break;
                        }


                        Manager activeManager =
                            managers[managerChoice - 1];

                        if (!Login(activeManager))
                        {
                            Console.WriteLine(
                                "Invalid credentials!"
                            );

                            break;
                        }


                        Console.WriteLine();
                        Console.WriteLine(
                            "------ Menedzser menu (" +
                            activeManager.GetName() +
                            ") ------"
                        );


                        activeManager.PrintEmployees();

                        break;


                    case 3:

                        Console.WriteLine();
                        Console.WriteLine(
                            "------ Employee-k ------"
                        );


                        foreach (Employee employee in employees)
                        {
                            Console.WriteLine(
                                employee.GetName() +
                                " - " +
                                employee.GetSalary()
                            );
                        }

                        break;


                    case 4:

                        Console.WriteLine();
                        Console.WriteLine(
                            "Osszes fizetes: " +
                            Employee.GetTotalSalaries()
                        );

                        break;


                    case 5:

                        menu = false;

                        Console.WriteLine(
                            "Kilepes a rendszerbol."
                        );

                        break;


                    default:

                        Console.WriteLine(
                            "Ervenytelen valasz!"
                        );

                        break;
                }
            }

            foreach (Employee employee in employees)
            {
                if (employee.GetName().Equals("Nagy Anna") && employee is Manager)
                {
                    Console.WriteLine("Nagy Anna megtalalva.");
                }
            }

        } 
    } 
} 