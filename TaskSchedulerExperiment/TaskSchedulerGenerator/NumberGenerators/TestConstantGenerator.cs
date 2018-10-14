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
        [TestCase(0.25f, ExpectedResult = 0.25f)]
        public float TestGenerateNumber(float inputNumber)
        {
            return new ConstantGenerator(inputNumber).GetNumber();
        }
        
    }
}
