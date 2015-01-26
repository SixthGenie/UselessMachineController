using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace UselessMachineController
{
    /// <summary>
    /// A Cross platform implementation which can delay time on the microsecond(μs) scale.
    /// It operates at a frequencies which are faster then most Platform Invoke results can provide due to the use of Kernel Calls under the hood.
    /// </summary>
    public sealed class μTimer 
    {
        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        static extern bool QueryPerformanceFrequency(out long lpFrequency);

        /// <summary>
        /// Performs a sleep using a plaform dependent but proven method
        /// </summary>
        /// <param name="amount">The amount of time to sleep in microseconds(μs)</param>
        public static void uSleep(TimeSpan amount) { μTimer.uSleep(((int)(amount.TotalMilliseconds * 1000))); }

        /// <summary>
        /// Performs uSleep by convention of waiting on performance couters
        /// </summary>
        /// <param name="waitTime">The amount of time to wait</param>
        public static void uSleep(int waitTime)
        {
            long time1 = 0, time2 = 0, freq = 0;

            QueryPerformanceCounter(out time1);
            QueryPerformanceFrequency(out freq);

            do
            {
                QueryPerformanceCounter(out time2);
            } while ((time2 - time1) < waitTime);
        }
        
    }
}
