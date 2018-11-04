using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerGenerator.NumberGenerators;

namespace TaskSchedulerGenerator.TaskIO
{
    public class Configuration : IConfiguration
    {
        public int SimulationLength { get; set; }

        public decimal SystemLoad { get; set; }

        public int MeanTaskLength { get; set; }

        public float CoefficientOfVariationTaskLength { get; set; }
        
        public int TickLength { get; set; }

        public string OutputPath { get; set; }

        public IMaxDelayGenerator MaxDelayGenerator { get; set; }

        public ITaskLengthGenerator TaskLengthGenerator { get; set; }

        public ITaskPerTickGenerator TaskPerTickGenerator { get; set; }

        public float CoefficientOfVariationMaxDelay { get; set; }

        public float MeanMaxDelay { get; set; }

        public float CoefficientOfVariationTaskPerTick { get; set; }
    }
}
