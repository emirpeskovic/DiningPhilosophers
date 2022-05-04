using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    // Our fork class to keep track of forks
    public class Fork
    {
        // Whether it's taken or not
        private bool Taken;

        // Who it's taken by (id of philosopher)
        private int TakenBy;

        public Fork()
        {
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
}
