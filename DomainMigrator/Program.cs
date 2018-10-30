using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace DomainMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var rand = RandomNumberGenerator.Create())
            {
                var key = new byte[64];
                rand.GetBytes(key);
                var result = Convert.ToBase64String(key);
                Trace.WriteLine(result);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
