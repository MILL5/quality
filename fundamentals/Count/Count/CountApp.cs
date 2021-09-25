using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Pineapple.Diagnostics;

namespace Count
{
    public partial class CountApp
    {
        private ILogger _logger;

        public CountApp(ILogger<CountApp> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
var people = GetPeople();

// BAD
Console.WriteLine($"Number of People: {people.Count()}");
// GOOD
Console.WriteLine($"Number of People: {people.Count}");

const int ITERATION = 100000;

using (var ts = new TimingScope("Count()", _logger))
{
    for (int i = 0; i < ITERATION; i++)
    {
        var count = people.Count();
    }
}

using (var ts = new TimingScope("Count", _logger))
{
    for (int i = 0; i < ITERATION; i++)
    {
        var count = people.Count;
    }
}

            _logger.Flush();

            Console.WriteLine("Hit any key to exit.");
            Console.ReadLine();
        }

        private static IList<Person> GetPeople()
        {
            return new List<Person> {
                                        new Person { FirstName = "Richard", LastName = "Crane"},
                                        new Person { FirstName = "Scott", LastName = "Hanselman" },
                                        new Person { FirstName = "Satya", LastName = "Nadella" },
                                        new Person { FirstName = "Scott", LastName = "Gu" }
                                    };
        }
    }
}
