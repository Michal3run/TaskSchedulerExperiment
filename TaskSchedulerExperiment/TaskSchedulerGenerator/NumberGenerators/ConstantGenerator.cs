using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.NumberGenerators
{
    class ConstantGenerator : INumberGenerator
    {
        float ConstantNumber;

        public ConstantGenerator(float constantNumber)
        {
            ConstantNumber = constantNumber;
        }

        public float GetNumber()
        {
            return ConstantNumber;
        }
    }
}
