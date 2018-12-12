using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using MathNet.Numerics.Statistics;

namespace TaskSchedulerGenerator.NumberGenerators
{
    [TestFixture]
    class TestUniformTaskPerTickGenerator
    {
        ITaskPerTickGenerator Generator;

        [SetUp]
        public void SetUp()
        {
            Generator = new UniformTaskPerTickGenerator();
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Test_With0Variation(int mean)
        {
            var numbers = GenerateNumbers(mean, 0);
            numbers.ShouldAllBe(x => x == mean);
        }

        [TestCase(2f,0.5f)]
        public void Test_WithVariation(float mean, float coefficientOfVariation)
        {
            var numbers = GenerateNumbers(mean, coefficientOfVariation);
            var dnumbers = numbers.Select(x => (double)x).ToList();
            var standardDeviation = Statistics.StandardDeviation(dnumbers);
            var calculatedMean = Statistics.Mean(dnumbers);
            (standardDeviation / calculatedMean).ShouldBe(coefficientOfVariation, 0.05f);
            
        }



        private IEnumerable<int> GenerateNumbers(float mean, float coefficientOfVariation)
        {
            Generator.Initialize(mean, coefficientOfVariation);
            var numbers = Enumerable.Range(0, 100)
                .Select(x => Generator.GetNumber());
            return numbers;
        }
    }
}
