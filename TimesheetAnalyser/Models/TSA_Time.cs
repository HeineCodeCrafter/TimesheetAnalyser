using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TimesheetAnalyser.Models
{
    internal class TSA_Time
    {
        public TSA_Time()
        {
            Project = new TSA_Project();  
        }
        public string Day { get; set; }
        public double Hours { get; set; }
        public TSA_Project Project { get; set; }

        
        public string Category { get; set; } 
         
        public string LineProperty { get; set; }


        public override string ToString()
        {
            return $" {Project.PID} {Project.PName}: {Hours} ({LineProperty})";
        }
    }
    public enum E_LinePropertyFilter
    { 
        CompTime,
        Vacation,
        OvertimeS,
        OvertimeQ
    }
}
