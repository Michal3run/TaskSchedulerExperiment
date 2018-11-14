using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace TaskSchedulerGenerator.NumberGenerators
{
    public class UniformTaskPerTickGenerator : ITaskPerTickGenerator
    {
        BinomialContinousN Distribution;

        public int GetNumber()
        {
            return Distribution.Sample();
        }

        public void Initialize(float mean, float coefficientOfVariation)
        {
            var p = 1 - coefficientOfVariation;
            var n = mean / p;
            Distribution = new BinomialContinousN(p, n);
        }

    }
}
