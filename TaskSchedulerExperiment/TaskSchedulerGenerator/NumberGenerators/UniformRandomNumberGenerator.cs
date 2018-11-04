using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.NumberGenerators
{
    public class UniformRandomNumberGenerator : IAnyGenerator
    {
        int MaximalValue;
        int MinimalValue;
        Random rand;

        public UniformRandomNumberGenerator()
        {
            rand = new Random();
        }

        public int GetNumber()
        {
            return rand.Next(MinimalValue, MaximalValue);
        }

        public void Initialize(float mean, float coefficientOfVariation)
        {
            //This is not correct from the point of view of mathematics but it looks more intuitive to me
            MaximalValue = Convert.ToInt32(Math.Floor((1 + coefficientOfVariation) * mean)) + 1;
            MinimalValue = Convert.ToInt32(Math.Ceiling((1 - coefficientOfVariation) * mean));
        }
    }
}
