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
                MaxDelayGenerator = new UniformRandomNumberGenerator(),
                TaskLengthGenerator = new UniformRandomNumberGenerator(),
                TaskPerTickGenerator = new UniformRandomNumberGenerator(),
                SimulationLength = 1000000,
                TickLength = 20,
                SystemLoad = 0.5m,

                MeanTaskLength = 5,
                CoefficientOfVariationTaskLength = 0.2f,

                MeanMaxDelay = 12,
                CoefficientOfVariationMaxDelay = 2f / 3f,

                CoefficientOfVariationTaskPerTick = 1f,

                OutputPath = @"..\..\..\Input\output.csv",
            };
            var generator = new TaskGenerator(configuration);
            generator.Generate();
        }
    }
}
