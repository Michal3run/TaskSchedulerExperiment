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
    class TestParetoTaskPerTickGenerator
    {
        [TestCase(5f, 1f)]
        [TestCase(5f, 10f)]
        [TestCase(5f, 50f)]
        [TestCase(1f, 1f)]
        [TestCase(1f, 10f)]
        [TestCase(1f, 50f)]
        [TestCase(1f, 100f)]
        [TestCase(1f, 200f)]
        [TestCase(0.1f, 1f)]
        [TestCase(0.1f, 10f)]
        [TestCase(0.1f, 50f)]
        [TestCase(0.1f, 100f)]
        [TestCase(0.1f, 200f)]
        public void TestParetoForRegularParameters(float mean, float RSD)
        {
            float cov = RSD;
            var v = cov * mean;
            var distribution = new ParetoTaskPerTickGenerator();
            distribution.Initialize(mean, cov);
            var list = Enumerable.Range(1, 1000000).Select(x => (double)distribution.GetNumber()).ToList();
            var calculatedMean = Statistics.Mean(list);
            calculatedMean.ShouldBe(mean, 0.05 * mean);
        }
    }
}
