using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.TaskIO
{
    interface IConfiguration
    {
        int SimulationLength { get; }
        decimal SystemLoad { get; }
        int MeanTaskLength { get; }
    }
}
