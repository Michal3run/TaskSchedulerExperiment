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
        [TestCase(10f, 0.5f)]
        [TestCase(10f, 3f)]
        [TestCase(10f, 7f)]
        [TestCase(10f, 15f)]
        public void TestParetoForRegularParameters(float mean, float RSD)
        {
            float cov = RSD;
            var v = cov * mean;
            var distribution = new ParetoTaskPerTickGenerator();
            distribution.Initialize(mean, cov);
            var list = Enumerable.Range(1, 1000000).Select(x => (double)distribution.GetNumber()).ToList();
            var calculatedMean = Statistics.Mean(list);
            calculatedMean.ShouldBe(mean, 0.05 * mean);
            (Statistics.StandardDeviation(list) / calculatedMean).ShouldBe(RSD, 0.5* RSD);
        }
    }
}
