using System;
using System.IO;

namespace day_one
{
    class Program
    {
        static void Main(string[] args)
        {

            var input = File.ReadAllLines("input.txt");
            foreach (var number1 in input)
            {
                foreach (var number2 in input)
                {
                    foreach (var number3 in input)
                    {
                        var a = int.Parse(number1);
                        var b = int.Parse(number2);
                        var c = int.Parse(number3);
                        if (a + b + c == 2020)
                        {
                            Console.WriteLine($"{number1} + {number2} + {number3} = 2020");
                            Console.WriteLine(a * b * c);
                            break;
                        }
                    }
                }
            }

        }
    }
}
