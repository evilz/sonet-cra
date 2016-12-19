using System;
using CommandLine;

namespace sonet.cra
{
    class Options
    {
        [Option('m', "month", Required = false, HelpText = "Month to be processed. (1 - 12)")]
        public int Month { get; set; } = DateTime.Today.Month;

        [Option('y', "year", Required = false, HelpText = "Year to be processed.")]
        public int Year { get; set; } = DateTime.Today.Year;
    }
}
