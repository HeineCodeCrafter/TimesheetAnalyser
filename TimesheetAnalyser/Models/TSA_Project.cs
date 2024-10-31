using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetAnalyser.Models
{
    internal class TSA_Project
    { 
        public string Customer { get; set; }
        public string Name { get; set; }
        public string PID { get; set; }
        public string PName { get; set; }
        public string ActivityNumber { get; set; }
        public string Activity { get; set; }

         
        public override string ToString()
        {
            return $" {PID} {PName} {Customer} {Name}";
        }
    }
}
