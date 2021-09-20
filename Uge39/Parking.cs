using System;
using System.Reflection.Emit;
using System.Threading;

namespace Uge39
{
    public class Parking
    {
        private readonly Object _entranceBlocked = new object();
        private readonly Object _exitBlocked = new object();
        private int isThereSpaceAvailable = 0;
        private int numberOfMaxThreads = 50;
        private int activeThreads = 0;
        private int carNumber = 0;
        private int maximumNumberOfParkingSpots = 10;

        public void CarStart()
        {
            var rand = new Random();
            while (activeThreads<numberOfMaxThreads)
            {
                ThreadStart threadStart = new ThreadStart(Entrance);
                Thread newthread = new Thread(threadStart);
                activeThreads++;
                newthread.Start();
                newthread.Name = $"car {carNumber}";
                carNumber++;
                Console.WriteLine($"{newthread.Name} started and is waiting to go to the entrance");
                
                Thread.Sleep(rand.Next(200,1000));
            }
                Entrance();
        }


        public void Entrance(){
            lock (_entranceBlocked)
            {
                Checkagain:
                if (isThereSpaceAvailable < maximumNumberOfParkingSpots)
                {
                    Thread thread = Thread.CurrentThread;
                    Console.WriteLine($"{thread.Name} is going through the entrance");
                    isThereSpaceAvailable++;
                }
                else
                {
                    //Thread.Sleep(2000);
                    goto Checkagain;
                }
            }
            InTheParking();
            

        }

        public  void InTheParking()
        {
            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"{thread.Name} is parked");
            var rand = new Random();
            Thread.Sleep(rand.Next(5000,20000));
            
            //time for exit
            ExitParking();
        }

        public  void ExitParking()
        {
            lock (_exitBlocked)
            {
                Thread thread = Thread.CurrentThread;
                Console.WriteLine($"{thread.Name} about to exit the parking area");
                Thread.Sleep(200);
                isThereSpaceAvailable--;
                activeThreads--;
                //carNumber--;

            }
            
        }


    }
}