using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerGenerator;
using TaskSchedulerGenerator.NumberGenerators;
using TaskSchedulerGenerator.TaskIO;

namespace TaskSchedulerGeneratorRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new Configuration
            {
                MaxDelayGenerator = new UniformRandomNumberGenerator(4,20),
                TaskLengthGenerator = new UniformRandomNumberGenerator(1, 3),
                TaskPerTickGenerator = new UniformRandomNumberGenerator(0, 10),
                SimulationLength = 10000,
                TickLength = 20,
                SystemLoad = 0.5m,
                MeanTaskLength = 5,
                OutputPath = @"..\..\..\Input\output.csv",

            };
            var generator = new TaskGenerator(configuration);
            generator.Generate();
        }
    }
}
