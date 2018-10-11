using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

namespace TaskSchedulerGenerator.VariableCalculators
{
    [TestFixture]
    class TestTaskQuantityCalculator
    {
        [TestCase(1000, 0.75, 25, ExpectedResult = 30)]
        [TestCase(2000, 0.5, 10, ExpectedResult = 100)]
        [TestCase(500, 0.2, 11, ExpectedResult = 9)]
        public int CalculateTaskQuantity(int simulationLength, decimal systemLoad, int meanTaskLength)
        {
            var calculator = new TaskQuantityCalculator();
            return calculator.CalculateTaskQuantity(simulationLength, systemLoad, meanTaskLength);
        }

    }
}
