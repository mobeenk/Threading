namespace App1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App1 started.");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"App1 processing step {i + 1}");
                Thread.Sleep(1000); // Simulate some work
            }

            Console.WriteLine("App1 completed.");
        }
    }
}