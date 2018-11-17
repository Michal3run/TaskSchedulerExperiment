using System;

namespace TaskSchedulerExperiment.Saver
{
    public class FileSaver : IResultSaver
    {
        public void Save(string result)
        {
            Console.WriteLine(result);
        }
    }
}
