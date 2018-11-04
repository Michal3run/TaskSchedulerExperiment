using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.NumberGenerators
{
    [TestFixture]
    class TestConstantGenerator
    {
        [TestCase(5, ExpectedResult = 5)]
        [TestCase(0.25f, ExpectedResult = 0f)]
        [TestCase(0.75f, ExpectedResult = 1f)]
        public float TestGenerateNumber(float inputNumber)
        {
            var constantGenerator = new ConstantGenerator();
            constantGenerator.Initialize(inputNumber, 0);
            return constantGenerator.GetNumber();
        }

    }
}
