using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = new FileReader("input.txt").ReadAllLines();
            var calculator = new Calculator(inputs, 3);
            var output = calculator.GetPartOneOutput();
            var secondOutput = calculator.GetPartTwoOutput();

            Console.WriteLine($"Part 1: {output}, Part 2: {secondOutput}");
            Console.ReadLine();
        }

        public class Calculator
        {
            private readonly List<int> _inputs;
            private readonly int _spreadSize;

            public Calculator(List<int> inputs, int spreadSize = 0)
            {
                _inputs = inputs;
                _spreadSize = spreadSize;
            }

            public int GetPartOneOutput()
            {
                var output = 0;
                var previousValue = 0;
                for (int index = 0; index < _inputs.Count(); index++)
                {
                    var value = _inputs[index];
                    if (index != 0 && value > previousValue)
                        output++;

                    previousValue = value;
                }

                return output;
            }   
            
            public int GetPartTwoOutput()
            {
                var previousSpreadSum = 0;
                var output = 0;
                for (int i = 0; i<_inputs.Count(); i++)
                {
                    var spread = GetSpreadFromIndex(i);
                    if (spread.Any())
                    {
                        if (previousSpreadSum != 0 && GetSpreadSum(spread) > previousSpreadSum)
                            output++;

                        previousSpreadSum = GetSpreadSum(spread);
                    }                  
                }

                return output;
            }

            private int GetSpreadSum(List<int> spread) => spread.Sum();

            private List<int> GetSpreadFromIndex(int index)
            {
                if (index + _spreadSize > _inputs.Count())
                    return new List<int>();

                return _inputs.GetRange(index, _spreadSize);
            }
        }

        public class FileReader
        {
            private readonly string _path;

            public FileReader(string path)
            {
                _path = path;
            }

            public List<int> ReadAllLines()
            {
                var inputs = File.ReadAllLines(_path);
                return inputs.Select(x => int.Parse(x)).ToList();
            }
        }
    }
}
