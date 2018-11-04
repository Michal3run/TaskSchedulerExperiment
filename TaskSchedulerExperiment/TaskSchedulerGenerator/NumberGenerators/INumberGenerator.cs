using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.NumberGenerators
{
    public interface INumberGenerator
    {
        void Initialize(float mean, float coefficientOfVariation);
        int GetNumber();
    }
}
