using System.IO;

namespace TaskSchedulerExperiment.Saver
{
    public class FileSaver : IResultSaver
    {
        public const string FileDirectory = @"..\..\..\Output";

        private static object _lock = new object();

        public void Save(string result)
        {
            lock (_lock)
            {
                using (var writer = new StreamWriter($"{FileDirectory}/result.txt", true))
                {
                    writer.WriteLine(result);
                }
            }
        }
    }
}
