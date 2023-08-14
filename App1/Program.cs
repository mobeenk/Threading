namespace App1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App1 started.");
            foreach (var item in args)
            {
                Console.WriteLine(item);
                Thread.Sleep(1000); // Simulate some work
            }

            Console.WriteLine("App1 completed.");
        }
    }
}