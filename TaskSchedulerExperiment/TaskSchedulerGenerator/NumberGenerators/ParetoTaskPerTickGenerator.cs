using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace TaskSchedulerGenerator.NumberGenerators
{
    public class ParetoTaskPerTickGenerator : ITaskPerTickGenerator
    {
        Pareto Distribution;
        double Sum;

        public int GetNumber()
        {
            double nextSample = Distribution.Sample();
            Sum += nextSample;
            double result = Math.Floor(Sum);
            Sum -= result;
            return (int)result;
        }

        public void Initialize(float mean, float coefficientOfVariation)
        {
            double m = mean;
            double sd = mean * coefficientOfVariation;
            double v = sd * sd;
            double a = 1 + Math.Sqrt(1 + m*m/v);
            double xm = m * (a - 1) / a;
            Distribution = new Pareto(xm, a);
            Sum = 0;
        }
    }
}
