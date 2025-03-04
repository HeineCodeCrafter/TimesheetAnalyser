using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VisualizedTimeSheets.Models
{
    internal class TSA_Time
    {
        public TSA_Time()
        {
            Project = new TSA_Project();  
        } 

        public bool IsWeekend
        {
            get {
                switch (Day.ToLower())
                {
                    case "mon":
                    case "tue":
                    case "wed":
                    case "thu":
                    case "fri":
                        return false;
                    case "sat":
                    case "sun":
                        return true;

                    default:
                        return false;
                        
                } 
            
            }

        }

        public string Day { get; set; }
        public double Hours { get; set; }
        public TSA_Project Project { get; set; }


        public string Category { get; set; } 
         
        public string LineProperty { get; set; }
        public bool ToMuch { get; internal set; }

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
