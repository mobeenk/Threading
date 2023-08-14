using System;
using System.Diagnostics;

namespace Threading
{
    public class Program
    {
        static string app1Path = @"D:\VS2022 Repos\Threading\App1\bin\Debug\net6.0\App1.exe";
        static string app2Path = @"D:\VS2022 Repos\Threading\App2\bin\Debug\net6.0\App2.exe";

        static async Task Main(string[] args)
        {
            List<Facility> facilities = Entities.Data();

           // NormalCalls(facilities);

              await AsyncCalls(facilities);
            //await RunAsync();
            //Run();
        }

        private static void NormalCalls(List<Facility> facilities)
        {
            foreach (var facility in facilities)
            {
                List<Employee> employees = Entities.GetEmployeesAsync(facility).Result;

                foreach (var employee in employees)
                {
                    Console.WriteLine($"Facility: {facility.FacilityName}, Employee: {employee.Name}, Age: {employee.Age}");
                }
            }
        }

        private static async Task AsyncCalls(List<Facility> facilities)
        {
            List<Task<List<Employee>>> tasks = new List<Task<List<Employee>>>();

            Stopwatch sw = Stopwatch.StartNew();
            

            foreach (var facility in facilities)
            {
                tasks.Add(Entities.GetEmployeesAsync(facility));
            }

            List<Employee>[] employeesLists = await Task.WhenAll(tasks);

            foreach (var employees in employeesLists)
            {
                foreach (var employee in employees)
                {
                    Console.WriteLine($"Facility: {employee.Name}, Age: {employee.Age}");
                }
            }
           
            sw.Stop();
            TimeSpan elapsedTime = sw.Elapsed;
            string formattedElapsedTime = elapsedTime.ToString(@"hh\:mm\:ss\.fff");
            Console.WriteLine($"Elapsed Time: {formattedElapsedTime}");
        }

        static async Task ProcessFacilityAsync(Facility facility)
        {
            Random random = new Random();
            int randomDelay = random.Next(1000, 3001); // Generates a random number between 1000 and 3000 milliseconds
            Thread.Sleep(randomDelay);
            await Task.Run(() =>
            {
                Console.WriteLine($"Processing facility: {facility.FacilityName}");
                
                foreach (var employee in facility.Employees)
                {
                    Console.WriteLine($"  Employee: {employee.Name}, Age: {employee.Age}");
                    // Perform any processing needed for each employee asynchronously
                }
            });
        }

        private static void Run()
        {
           
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            RunConsoleApp(app1Path, "app1-arg1", "app1-arg2");
            RunConsoleApp(app2Path, "app2-arg1", "app2-arg2");

            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;

            string formattedElapsedTime = elapsedTime.ToString(@"hh\:mm\:ss\.fff");

            Console.WriteLine("Both apps have completed.");
            Console.WriteLine($"Elapsed Time: {formattedElapsedTime}");
        }

        private static async Task RunAsync()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Task app1Task = RunConsoleAppAsync(app1Path, "arg1", "arg2", "arg3", "arg4", "arg5");
            Task app2Task = RunConsoleAppAsync(app2Path, "app2-arg1", "app2-arg2");
            Task asyncMethod = YourAsyncMethod();
            await Task.WhenAll(app1Task, app2Task, asyncMethod);
            sw.Stop();
            TimeSpan elapsedTime = sw.Elapsed;
            string formattedElapsedTime = elapsedTime.ToString(@"hh\:mm\:ss\.fff");

            Console.WriteLine("Both apps have completed.");
            Console.WriteLine($"Elapsed Time: {formattedElapsedTime}");
            Console.WriteLine("Both apps have completed.");
        }

        static async Task RunConsoleAppAsync(string appPath, params string[] arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = appPath,
                Arguments = string.Join(" ", arguments),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                        Console.WriteLine($"[App Output] {e.Data}");
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                        Console.WriteLine($"[App Error] {e.Data}");
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await process.WaitForExitAsync();
            }
        }
        static void RunConsoleApp(string appPath, params string[] arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = appPath,
                Arguments = string.Join(" ", arguments),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                        Console.WriteLine($"[App Output] {e.Data}");
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                        Console.WriteLine($"[App Error] {e.Data}");
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();
            }
        }

        private static async Task YourAsyncMethod()
        {
            // Your asynchronous method implementation
            Thread.Sleep(1000); // Simulate some work
            Console.WriteLine("Finished AsyncMethod");
        }
    }
}