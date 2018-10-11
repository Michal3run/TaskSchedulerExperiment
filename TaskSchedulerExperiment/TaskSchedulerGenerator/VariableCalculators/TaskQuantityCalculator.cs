using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.VariableCalculators
{
    class TaskQuantityCalculator : ITaskQuantityCalculator
    {
        public int CalculateTaskQuantity(int simulationLength, decimal systemLoad, int meanTaskLength)
        {
            return Convert.ToInt32(simulationLength * systemLoad / meanTaskLength);
        }
    }
}
