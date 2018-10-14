using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerGenerator.NumberGenerators;

namespace TaskSchedulerGenerator.TaskIO
{
    interface IConfiguration
    {
        int SimulationLength { get; }
        decimal SystemLoad { get; }
        int MeanTaskLength { get; }
        int TickLength { get; }
        INumberGenerator TaskLengthGenerator { get; }
        INumberGenerator MaxDelayGenerator { get; }
        INumberGenerator TaskPerTickGenerator { get; }
    }
}
