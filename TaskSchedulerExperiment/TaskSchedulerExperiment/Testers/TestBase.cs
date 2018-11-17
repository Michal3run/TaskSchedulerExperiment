using TaskSchedulerExperiment.Saver;

namespace TaskSchedulerExperiment
{
    public abstract class TestBase
    {
        public const string FileDirectory = @"..\..\..\Input";

        protected TestBase(IResultSaver saver)
        {
            Saver = saver;
        }

        public abstract void Test();

        protected IResultSaver Saver { get; }

        protected string GetFileWithPath(string fileName) => $"{FileDirectory}\\{fileName}";
    }
}
