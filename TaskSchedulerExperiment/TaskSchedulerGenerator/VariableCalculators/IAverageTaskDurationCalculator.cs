using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.VariableCalculators
{
    interface IAverageTaskDurationCalculator
    {
        float CalculateAverageTaskDuration(int quantityOfTasks, int simulationDuration);
    }
}
