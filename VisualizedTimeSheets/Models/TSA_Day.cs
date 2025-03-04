using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizedTimeSheets.Models
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


        public string Date { get { return $"{TimeStamp:yyyy.MM.dd}";} }
        public string DayOfWeek { get { return $"{TimeRegister.FirstOrDefault().Day}"; } }

        public string FoundIn { get; set; }

        public override string ToString()
        {
            return $"{TimeStamp:dd.MM.yyyy}  {TotalHours}";
        }


        public double SumVacation {
            get {  return GetHourSumForType(E_LinePropertyFilter.Vacation); }
        }
        public double SumCompTime
        {
            get { return GetHourSumForType(E_LinePropertyFilter.CompTime); }
        }
        public double SumOvertimeS
        {
            get { return GetHourSumForType(E_LinePropertyFilter.OvertimeS); }
        }

        public double SumOvertimeQ
        {
            get { return GetHourSumForType(E_LinePropertyFilter.OvertimeQ); }
        }
        public double SumOvertimeTotal
        {
            get { return GetHourSumForType(E_LinePropertyFilter.OvertimeS)+ GetHourSumForType(E_LinePropertyFilter.OvertimeQ); }
        }


        internal double GetHourSumForType(E_LinePropertyFilter linePropertyFilter)
        {
            double sum = 0;
            double normalHourOffset = 7.5;
            double negativeCompensation = 0;
            double overtime_compare_to_comptime = 0.0;
            foreach (var entry in TimeRegister)
            {
                // Skip weekends if necessary for all cases
                if (entry.IsWeekend) 
                {
                    normalHourOffset = 0.0;
                }
                
                switch (linePropertyFilter)
                {
                    case E_LinePropertyFilter.Vacation:
                        sum += CalculateVacationHours(entry); 
                        if (sum > 0) return sum; // Exit early if vacation hours found
                        break;

                    case E_LinePropertyFilter.CompTime:
                        CalculateCompensationHours(entry, ref sum, ref negativeCompensation, normalHourOffset);

                        overtime_compare_to_comptime = CalculateOvertimeHours(entry, "overtime");
                        if (sum > normalHourOffset)
                        {
                            entry.ToMuch = true;
                        }
                        break;

                    case E_LinePropertyFilter.OvertimeS:
                        sum += CalculateOvertimeHours(entry, "overtime-s");
                        if (sum > normalHourOffset)
                        {
                            entry.ToMuch = true;
                        }
                        break;

                    case E_LinePropertyFilter.OvertimeQ:
                        sum += CalculateOvertimeHours(entry, "overtime-q");
                        if (sum > normalHourOffset)
                        {
                            entry.ToMuch = true;
                        }
                        break;
                }

            }

            // Adjust sum for CompTime case
            if (linePropertyFilter == E_LinePropertyFilter.CompTime)
            {
                sum -= normalHourOffset + negativeCompensation;

                if (sum * -1 == overtime_compare_to_comptime)
                {
                    sum = sum + overtime_compare_to_comptime;

                }
            }
             
            return sum;
        }

        private double CalculateVacationHours(TSA_Time entry)
        {
            //if (entry.Category.Contains("vacation", StringComparison.OrdinalIgnoreCase) && entry.Hours != 7.5)

            if (entry.Category.ToLower().Contains("vacation"))
            {
                return entry.Hours;
            }
            //if (entry.Category.ToLower().Contains("vacation") && entry.Hours != 7.5)
            //{
            //    return 1; // Add 1 hour for vacation if criteria match
            //}
            return 0;
        }

        private void CalculateCompensationHours(TSA_Time entry, ref double sum, ref double negativeComp, double normalHourOffset)
        {
            //if (entry.LineProperty.Contains("overtime", StringComparison.OrdinalIgnoreCase)) return;
            if (entry.LineProperty.ToLower().Contains("overtime")) return;

            if (entry.Project.PID == "AB000001" && entry.Category.ToLower().Contains("comp time") )
            {
                negativeComp += entry.Hours;
            }
            sum += entry.Hours;
        }

        private double CalculateOvertimeHours(TSA_Time entry, string overtimeType)
        {
            //return entry.LineProperty.Contains(overtimeType, StringComparison.OrdinalIgnoreCase) ? entry.Hours : 0;
            return entry.LineProperty.ToLower().Contains(overtimeType) ? entry.Hours : 0;
        }
        internal double GetHourSumForType_okld(E_LinePropertyFilter linepropertyy)
        {
            double sum = 0;

            double normalHourOffset = 7.5;
  
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
                    //double normalHourOffset = 7.5;
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
