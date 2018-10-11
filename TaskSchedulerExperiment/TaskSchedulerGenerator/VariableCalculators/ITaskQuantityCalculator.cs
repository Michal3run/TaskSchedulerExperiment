using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.VariableCalculators
{
    interface ITaskQuantityCalculator
    {
        int CalculateTaskQuantity(int simulationLength, decimal systemLoad, int meanTaskLength);
    }
}
