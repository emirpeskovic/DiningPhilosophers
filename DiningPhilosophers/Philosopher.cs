using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    // Philosopher class
    public class Philosopher
    {
        // Id of the current Philosopher
        public int Id { get; set; }

        // Readonly reference to the forks
        public readonly Fork LeftFork;
        public readonly Fork RightFork;

        public Philosopher(int id, Fork leftFork, Fork rightFork)
        {
            Id = id;
            LeftFork = leftFork;
            RightFork = rightFork;
        }

        public void TryEat()
        {
            while (true)
            {
                // Check if we can take the forks
                if (LeftFork.Take(Id) && RightFork.Take(Id))
                {
                    // We got both forks, so we eat!
                    Console.WriteLine("Philosopher {0} is eating", Id);
                    Thread.Sleep(1000);

                    // Afterwards we release the forks
                    LeftFork.Release(Id);
                    RightFork.Release(Id);
                }
                else
                {
                    // One or both of the forks could not be taken
                    // In case one could be taken, we make sure to release it so it can be taken by another philosopher
                    LeftFork.Release(Id);
                    RightFork.Release(Id);

                    Console.WriteLine("Philosopher {0} is thinking", Id);
                    Thread.Sleep(1000);
                }

                Thread.Sleep(100 / 15);
            }
        }
    }
}
