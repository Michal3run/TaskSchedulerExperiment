using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerGenerator.NumberGenerators;

namespace TaskSchedulerGenerator.TaskIO
{
    public interface IConfiguration
    {
        int SimulationLength { get; }
        decimal SystemLoad { get; }
        int MeanTaskLength { get; }
        float CoefficientOfVariationTaskLength { get; }
        int TickLength { get; }
        string OutputPath { get; }
        IMaxDelayGenerator MaxDelayGenerator { get; }
        ITaskLengthGenerator TaskLengthGenerator { get; }
        ITaskPerTickGenerator TaskPerTickGenerator { get; }
        float CoefficientOfVariationMaxDelay { get; }
        float MeanMaxDelay { get; }
        float CoefficientOfVariationTaskPerTick { get; }
    }
}
