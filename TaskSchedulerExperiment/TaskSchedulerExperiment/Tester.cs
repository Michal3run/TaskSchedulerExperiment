using System;
using TaskSchedulerExperiment.Saver;

namespace TaskSchedulerExperiment
{
    class Tester
    {
        private static int _parametersCounter;
        private static IResultSaver _saver;

        private static void Main()
        {
            Console.WriteLine($"Please specify parameters 1. Saver (c/f) 2. Test class (one/all) For example: \"c all\"");
            var line = Console.ReadLine();
            var args = !string.IsNullOrEmpty(line) ? line.Split(' ') : new string[0];


            _saver = GetSaverSpeficicOfDefaultClass(args);
            TestBase testClass = GetTestSpecificOrDefaultClass(args);

            testClass.Test();

            Console.WriteLine("Testing finished!");
            Console.WriteLine($"Press any key to exit");
            Console.ReadKey();
        }

        private static IResultSaver GetSaverSpeficicOfDefaultClass(string[] args)
        {
            IResultSaver saver;

            if (args.Length <= _parametersCounter)
            {
                saver = new ConsoleSaver();
                Console.WriteLine($"Saver class was not defined. Chosen default class: {saver.GetType().Name}");
            }
            else
            {
                saver = GetSaverClass(args);
            }

            return saver;
        }

        private static IResultSaver GetSaverClass(string[] args)
        {
            var selectedTester = args[_parametersCounter++];

            switch (selectedTester)
            {
                case "c": return new ConsoleSaver();
                case "f": return new FileSaver();
                default: throw new Exception($"Unknown tester! {selectedTester}");
            }
        }

        private static TestBase GetTestSpecificOrDefaultClass(string[] args)
        {
            TestBase testClass;

            if (args.Length <= _parametersCounter)
            {
                testClass = new AllFilesTester(_saver);
                Console.WriteLine($"Tester class was not defined. Chosen default class: {testClass.GetType().Name}");
            }
            else
            {
                testClass = GetTestClass(args);
            }

            return testClass;
        }

        private static TestBase GetTestClass(string[] args)
        {
            var selectedTester = args[_parametersCounter++];

            switch (selectedTester)
            {
                case "one": return GetOneFileTester(args);
                case "all": return new AllFilesTester(_saver);
                default: throw new Exception($"Unknown tester! {selectedTester}");
            }
        }

        private const string DefaultFile = "0,9_40_5";

        private static OneFileTester GetOneFileTester(string[] args)
        {
            string fileName;

            if (args.Length <= _parametersCounter)
            {
                Console.WriteLine($"File was not specified for one file tester. Chosen default: {DefaultFile}");
                fileName = DefaultFile;
            }
            else
            {
                fileName = args[_parametersCounter];
            }

            return new OneFileTester(_saver, fileName);
        }
    }
}
