using System;

namespace JobDependencySorter
{
    class Program
    {
        private const string EXIT_STRING = "EXIT";

        static void Main(string[] args)
        {
            Console.WriteLine($"Input your jobs here line by line in this formt \"a => b\" " +
                $"(type {EXIT_STRING} to terminate): ");
            var line = string.Empty;
            while ((line = Console.ReadLine()) != null)
            {
                if (line.ToUpper() == EXIT_STRING)
                {
                    break;
                }
            }
        }
    }
}
