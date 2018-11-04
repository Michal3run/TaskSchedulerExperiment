using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.NumberGenerators
{
    public class ConstantGenerator : IAnyGenerator
    {
        int ConstantNumber;
        
        public int GetNumber()
        {
            return ConstantNumber;
        }

        public void Initialize(float mean, float coefficientOfVariation)
        {
            ConstantNumber = Convert.ToInt32(mean);
        }
    }
}
