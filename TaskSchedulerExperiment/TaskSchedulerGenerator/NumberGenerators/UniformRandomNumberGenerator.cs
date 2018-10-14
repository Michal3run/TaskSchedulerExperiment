using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.NumberGenerators
{
    class UniformRandomNumberGenerator : INumberGenerator
    {
        float MaximalValue;
        float MinimalValue;
        Random rand;

        public UniformRandomNumberGenerator(float minimalValue, float maximalValue)
        {
            MaximalValue = maximalValue;
            MinimalValue = minimalValue;
            rand = new Random();
        }

        public float GetNumber()
        {
            var generatedRandom = MinimalValue + rand.NextDouble() * (MaximalValue - MinimalValue);
            return (float)generatedRandom;
        }
    }
}
