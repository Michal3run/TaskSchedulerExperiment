using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.VariableCalculators
{
    class AverageTaskDurationCalculator : IAverageTaskDurationCalculator
    {
        public float CalculateAverageTaskDuration(int quantityOfTasks, int simulationDuration)
        {
            return (float)simulationDuration / (float)quantityOfTasks;
        }
    }
}
