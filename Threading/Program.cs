using System.Diagnostics;

namespace Threading
{
    public class Program
    {
        static string app1Path = @"D:\VS2022 Repos\Threading\App1\bin\Debug\net6.0\App1.exe";
        static string app2Path = @"D:\VS2022 Repos\Threading\App2\bin\Debug\net6.0\App2.exe";

        static async Task Main(string[] args)
        {
            await RunAsync();
            //Run();
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
            Task app1Task = RunConsoleAppAsync(app1Path, "app1-arg1", "app1-arg2");
            Task app2Task = RunConsoleAppAsync(app2Path, "app2-arg1", "app2-arg2");

            await Task.WhenAll(app1Task, app2Task);
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
    }
}