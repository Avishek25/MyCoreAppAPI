using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusApi.Models
{
    public class CalculationOutcome
    {
        public short Progress { get; set; }
        public string Outcome { get; set; }
    }
}
