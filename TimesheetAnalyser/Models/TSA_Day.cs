using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetAnalyser.Models
{
    internal class TSA_Day
    {
        public TSA_Day()
        {
            TimeRegister = new List<TSA_Time>();

        } 

 
        public double TotalHours => TimeRegister.Sum(t => t.Hours); 

        public DateTime TimeStamp { get; set; }

        public List<TSA_Time> TimeRegister { get; set; }



        public override string ToString()
        {
            return $" {TimeStamp:dd.MM.yyyy}  {TotalHours}";
        }

        internal double GetHourSumForType(E_LinePropertyFilter linepropertyy)
        {
            double sum = 0;


            double negativeComp = 0;
            switch (linepropertyy)
            {
                case E_LinePropertyFilter.Vacation:
                    foreach (var item in TimeRegister)
                    {
                        if (item.Category.ToLower().Contains("vacation"))
                        {
                            if (item.Hours != 7.5)
                            {

                            }
                            sum += 1;
                            return sum;

                        }
                    }
                    break;
                case E_LinePropertyFilter.CompTime:
                    double normalHourOffset = 7.5;
                    foreach (var item in TimeRegister)
                    {
                        if (item.LineProperty.ToLower().Contains("overtime"))
                        {
                            continue;

                        }

                        if (item.Project.PID == "AB000001")
                        {
                            if (item.Category.ToLower().Contains("comp time"))
                            {
                                negativeComp += item.Hours;
                            }
                            
                        }


                        sum += item.Hours;
                    }
                    sum -= normalHourOffset;

                    sum -= negativeComp;

                    break;
                case E_LinePropertyFilter.OvertimeS:
                    foreach (var item in TimeRegister)
                    {
                        if (item.LineProperty.ToLower().Contains("overtime-s"))
                        {
                            sum += item.Hours;
                        }
                    }
                    break;
                case E_LinePropertyFilter.OvertimeQ:
                    foreach (var item in TimeRegister)
                    {
                        if (item.LineProperty.ToLower().Contains("overtime-q"))
                        {
                            sum += item.Hours;
                        }
                    }
                    break;
            }

            return sum;
        }
    }
}
