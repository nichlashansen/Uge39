using System;
using System.Threading;

namespace Uge39
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to europark");
            var parking = new Parking();
            parking.CarStart();
        }

    }


}