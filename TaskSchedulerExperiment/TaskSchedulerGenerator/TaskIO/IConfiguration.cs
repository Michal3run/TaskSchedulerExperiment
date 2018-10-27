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
        int TickLength { get; }
        string OutputPath { get; }
        IMaxDelayGenerator MaxDelayGenerator { get; }
        ITaskLengthGenerator TaskLengthGenerator { get; }
        ITaskPerTickGenerator TaskPerTickGenerator { get; }
    }
}
