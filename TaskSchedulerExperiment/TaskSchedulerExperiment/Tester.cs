using System;

namespace TaskSchedulerExperiment
{
    class Tester
    {
        private static void Main(string[] args)
        {
            TestBase testClass;

            if (args.Length < 1)
            {
                testClass = new AllFilesTester();
                Console.WriteLine($"Tester class was not defined. Chosen default class: {testClass.GetType().Name}");
            }
            else
            {
                testClass = GetTestClass(args);
            }

            testClass.Test();

            Console.WriteLine("Testing finished!");
            Console.WriteLine($"Press any key to exit");
            Console.ReadKey();
        }

        private static TestBase GetTestClass(string[] args)
        {
            var selectedTester = args[0];

            switch (selectedTester)
            {
                case "one": return GetOneFileTester(args);
                case "all": return new AllFilesTester();
                default: throw new Exception($"Unknown tester! {selectedTester}");
            }
        }

        private const string DefaultFile = "0,9_40_5";

        private static OneFileTester GetOneFileTester(string[] args)
        {
            string fileName;

            if (args.Length < 2)
            {
                Console.WriteLine($"File was not specified for one file tester. Chosen default: {DefaultFile}");
                fileName = DefaultFile;
            }
            else
            {
                fileName = args[1];
            }

            return new OneFileTester(fileName);
        }
    }
}
