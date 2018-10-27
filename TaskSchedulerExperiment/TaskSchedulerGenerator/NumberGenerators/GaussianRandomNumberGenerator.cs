using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.NumberGenerators
{
    public class GaussianRandomNumberGenerator : IAnyGenerator
    {
        float StdDev;
        float Mean;
        Random Rand;

        public GaussianRandomNumberGenerator(float stdDev, float mean)
        {
            StdDev = stdDev;
            Mean = mean;
            Rand = new Random();
        }

        public float GetNumber()
        {
             //reuse this if you are generating many
            double u1 = 1.0 - Rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - Rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = Mean + StdDev * randStdNormal; //random normal(mean,stdDev^2)
            return (float)randNormal;
        }
    }
}
