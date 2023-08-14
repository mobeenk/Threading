using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    public static class Entities
    {
        public static List<Facility> Data()
        {
            return new List<Facility>
            {
                new Facility
                {
                    FacilityName = "Facility 1",
                    Employees = new List<Employee>
                    {
                        new Employee { Name = "John", Age = 30 },
                        new Employee { Name = "Alice", Age = 25 }
                        // Add more employees
                    }
                },
                new Facility
                {
                    FacilityName = "Facility 2",
                    Employees = new List<Employee>
                    {
                        new Employee { Name = "Bob", Age = 28 },
                        new Employee { Name = "Eva", Age = 22 }
                        // Add more employees
                    }
                },
                new Facility
                {
                    FacilityName = "Facility 3",
                    Employees = new List<Employee>
                    {
                        new Employee { Name = "Michael", Age = 35 },
                        new Employee { Name = "Sophia", Age = 29 }
                        // Add more employees
                    }
                }
            };
        }
        public static int DelayRandomly()
        {
            Random random = new Random();
            int randomDelay = random.Next(3000, 6001); // Generates a random number between 1000 and 3000 milliseconds
            return randomDelay;
        }
        public static async Task<List<Employee>> GetEmployeesAsync(Facility facility)
        {
           // await Task.Delay(DelayRandomly()); // Simulate some asynchronous work
            await Task.Delay(3000); // Simulate some asynchronous work
            return facility.Employees;
        }
    }
    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Facility
    {
        public string FacilityName { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
