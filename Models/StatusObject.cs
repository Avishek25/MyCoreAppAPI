using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusApi.Models
{
    public class StatusObject
    {
        public Status Status { get; set; }
        public string Progress { get; set; }
        public string Outcome { get; set; }
    }

    public enum Status
    {
        Running,
        Completed,
        Failed
    }
}

