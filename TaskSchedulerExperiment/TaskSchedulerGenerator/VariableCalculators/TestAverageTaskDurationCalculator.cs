using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.VariableCalculators
{
    [TestFixture]
    class TestAverageTaskDurationCalculator
    {
        [TestCase(100, 1000, ExpectedResult = 10)]
        [TestCase(1000, 100, ExpectedResult = 0.1f)]
        public float CalculateAverageTaskDuration(int quantityOfTasks, int simulationDuration)
        {
            var calculator = new AverageTaskDurationCalculator();
            return calculator.CalculateAverageTaskDuration(quantityOfTasks, simulationDuration);
        }
    }
}
