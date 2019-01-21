using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.NumberGenerators
{
    [TestFixture]
    class TestProbabilityDistributions
    {
        [TestCase(5f, 2.5f)]
        [TestCase(5f, 2.005f)]
        [TestCase(5f, 20f)]
        public void TestDistribution(double xm, double a)
        {
            var expectedMean = a * xm / (a - 1);
            var expectedVariance = (xm * xm * a) / ((a - 1) * (a - 1) * (a - 2));
            var distribution = new Pareto(xm, a);
            var list = Enumerable.Range(1, 10000000).Select(x => distribution.Sample()).ToList();
            var calculatedMean = Statistics.Mean(list);
            calculatedMean.ShouldBe(expectedMean, 0.05 * expectedMean);
            Statistics.Variance(list).ShouldBe(expectedVariance, 0.5 * expectedVariance);
            
        }
    }
}
