namespace App2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App2 started.");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"App2 processing step {i + 1}");
                Thread.Sleep(1500); // Simulate some work
            }

            Console.WriteLine("App2 completed.");
        }
    }
}