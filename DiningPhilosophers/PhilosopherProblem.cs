using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    public class PhilosopherProblem
    {
        // Our fork class to keep track of forks
        public class Fork
        {
            // Id of the fork
            public int Id { get; set; }

            // Whether it's taken or not
            public bool Taken { get; set; }

            // Who it's taken by (id of philosopher)
            public int TakenBy { get; set; }

            public Fork(int id)
            {
                Id = id;
                Taken = false;
            }

            // We lock the fork and take it if we can
            // We make sure the philosopher id is set to the philosopher who is using it
            public bool Take(int philosopherId)
            {
                lock (this)
                {
                    if (Taken)
                    {
                        return false;
                    }
                    Taken = true;
                    TakenBy = philosopherId;
                    return true;
                }
            }

            // We release the fork only if it's taken by the philosopher who is releasing it
            public void Release(int philosopherId)
            {
                lock (this)
                {
                    if (TakenBy == philosopherId)
                    {
                        Taken = false;
                    }
                }
            }
        }

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

        public static void Run()
        {
            var forks = new List<Fork>();
            for (int i = 0; i < 5; i++)
            {
                forks.Add(new Fork(i));
            }

            var philosophers = new List<Philosopher>();
            for (int i = 0; i < 5; i++)
            {
                philosophers.Add(new Philosopher(i, forks[i], forks[(i + 1) % 5]));
            }

            var threads = new List<Thread>();

            foreach (var philosopher in philosophers)
            {
                var thread = new Thread(() => philosopher.TryEat());
                thread.Start();
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
    }
}
