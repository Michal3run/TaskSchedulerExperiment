using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerGenerator.DummyNs
{
    [TestFixture]
    class TestDummy
    {
        [Test]
        public void TestGetTrue()
        {
            var dummy = new Dummy();
            dummy.ReturnTrue().ShouldBeTrue();
        }
    }
}
