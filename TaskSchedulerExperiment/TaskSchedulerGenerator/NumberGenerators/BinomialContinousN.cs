using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.NumberGenerators
{
    class BinomialContinousN
    {
        Binomial LowerDistribution;
        Binomial UpperDistribution;
        Random Random;
        double fractialPart;

        public BinomialContinousN(float p, float n)
        {
            Random = new Random();
            LowerDistribution = new Binomial(p, (int)Math.Floor(n));
            UpperDistribution = new Binomial(p, (int)Math.Ceiling(n));
            fractialPart = n - Math.Truncate(n);
        }

        public int Sample()
        {
            if(Random.NextDouble() > fractialPart)
            {
                return LowerDistribution.Sample();
            }
            else
            {
                return UpperDistribution.Sample();
            }
        }
    }
}
