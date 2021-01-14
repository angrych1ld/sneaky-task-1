using System;
using System.Collections.Generic;
using System.Linq;
using DoerLib;

namespace DoerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create doer object
            Doer<int> doer = new Doer<int>();

            // Populate the doer with rules
            doer
                .On(i => i % 3 == 0 && i % 5 == 0, i => Console.WriteLine("SneakyBox"))
                .On(i => i % 3 == 0, i => Console.WriteLine("Sneaky"))
                .On(i => i % 5 == 0, i => Console.WriteLine("Box"))
                .OnElse(i => Console.WriteLine(i));

            // Have some data set
            IEnumerable<int> dataSet = Enumerable.Range(1, 100);

            // Run the doer
            doer.Do(dataSet);
        }
    }
}
