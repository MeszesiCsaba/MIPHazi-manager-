using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MIPHazi
{
    public class InputThread
    {
        private Thread thread;
        private ConcurrentQueue<string> inputQueue = new ConcurrentQueue<string>();

        public InputThread()
        {
            thread = new Thread(ReadInput);
            thread.Start();
        }

        private void ReadInput()
        {
            while(true)
            {
                string input = Console.ReadLine();

                if(input != null)
                {
                    inputQueue.Enqueue(input);
                }
            }
        }
        public string GetInput()
        {
            string input;

            while(!inputQueue.TryDequeue(out input))
            {
                Thread.Sleep(10);
            }

            return input;
        }
    }
}
