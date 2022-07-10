using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Complexity
{
    public delegate void delOnTick(double msElapsed);
    class CTimer
    {
        //event
        public event delOnTick OnTick;

        //API interface
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        //Private data members
        private long startTime, stopTime;
        private long freq;
        public bool isRunning { get; private set; }
        private uint interval;
        private System.Threading.Timer thrTimer;
        private long lastTick;

        // Constructor
        public CTimer(uint _interval)
        {
            startTime = 0;
            stopTime = 0;
            isRunning = false;
            interval = _interval;

            //Get frequency
            if (QueryPerformanceFrequency(out freq) == false)
                // High-performance counter not supported
                throw new Win32Exception();
        } //Constructor

        // Start the timer
        public void Start()
        {
            //Create timer to poll duration every 1 ms
            TimerCallback cbTimer = this.Tick;
            thrTimer = new System.Threading.Timer(cbTimer, null, 0, 1); //Interval small enough so that it fits into any possible frame capturing interval
            lastTick = 0;

            isRunning = true;
            QueryPerformanceCounter(out startTime);
        } //Start

        // Stop the timer
        public void Stop()
        {
            if (isRunning)
            {
                thrTimer.Dispose();
                isRunning = false;
                QueryPerformanceCounter(out stopTime);
            }
        } //Stop

        // Returns the duration of the timer (in seconds)
        public double msDuration
        {
            get
            {
                if (isRunning)
                {
                    long currentTime;
                    QueryPerformanceCounter(out currentTime);
                    return 1000 * (double)(currentTime - startTime) / (double)freq;
                }
                else
                    return 1000 * (double)(stopTime - startTime) / (double)freq;
            }
        }//Duration

        //Tick for thread timer 
        //- executes every 1 ms and check if the previous interval has elapsed
        private void Tick(Object stateInfo)
        {
            try
            {
                //System.Windows.Forms.Application.DoEvents(); Kan hy wees!!
                long ms = (long)(msDuration + 0.5);
                long ticks = ms / interval; //Integer division - throw away remainder
                if (ticks > lastTick)
                {
                    lastTick = ticks;
                    if (OnTick != null)
                        OnTick(msDuration);
                }
            }
            catch (Exception e)
            {
                //Msg.ShowError(System.Windows.Forms.Application.OpenForms[0], "CTimer//Tick: " + e.Message);
                //Stop();
            }
        } //Tick

    }//class CTimer
}//namespace