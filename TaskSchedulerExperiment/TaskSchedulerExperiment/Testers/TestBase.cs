namespace TaskSchedulerExperiment
{
    public abstract class TestBase
    {
        public const string FileDirectory = @"..\..\..\Input";

        public abstract void Test();

        protected string GetFileWithPath(string fileName) => $"{FileDirectory}\\{fileName}";
    }
}
