using DiningPhilosophers;

var forks = new Fork[5];
for (int i = 0; i < 5; i++)
{
    forks[i] = new Fork();
}

var philosophers = new Philosopher[5];
for (int i = 0; i < 5; i++)
{
    philosophers[i] = new Philosopher(i, forks[i], forks[(i + 1) % 5]);
}

var threads = new Thread[5];
for (int i = 0; i < 5; i++)
{
    threads[i] = new Thread(philosophers[i].TryEat);
    threads[i].Start();
}

foreach (var thread in threads)
{
    thread.Join();
}