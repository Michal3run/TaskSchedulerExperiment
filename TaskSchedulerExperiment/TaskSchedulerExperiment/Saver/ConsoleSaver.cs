using System;

namespace TaskSchedulerExperiment.Saver
{
    public class ConsoleSaver : IResultSaver
    {
        public void Save(string result)
        {
            Console.WriteLine(result);
        }
    }
}
