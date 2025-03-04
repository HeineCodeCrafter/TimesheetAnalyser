using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizedTimeSheets.Models
{
    internal class TSA_Plot
    {
        public List<TSA_Plot_Coord> Range = new List<TSA_Plot_Coord>();
        
        public TSA_Plot()
        {
            //Range = new List<TSA_Plot_Coord>();
            //Range.Add(first);
        }
        public void Append(TSA_Plot_Coord coord)
        {
            if (Range == null)
            {
                Range = new List<TSA_Plot_Coord>();
            }

            Range.Add(coord); 
        }
        // Read-only property to get the first time entry
        public TSA_Plot_Coord First
        {
            get
            {
                return Range.FirstOrDefault();
            }
        }
        
        // Read-only property to get the first time entry
        public TSA_Plot_Coord Last
        {
            get
            {
                return Range.LastOrDefault(); 
            }
        }
         
        //public TSA_Plot_Coord HighestValue
        //{
        //    get
        //    {
        //        TSA_Plot_Coord result = null;
        //        foreach (TSA_Plot_Coord item in Range)
        //        {
        //            if (result==null)
        //            {
        //                result = item;
        //                continue;
        //            }
        //            if (item.Y > result.Y)
        //            {
        //                result = item;
        //                continue; 
        //            } 
        //        }
        //        return result;
        //    }
        //}
        //public TSA_Plot_Coord LowestValue
        //{
        //    get
        //    {
        //        TSA_Plot_Coord result = null;
        //        foreach (TSA_Plot_Coord item in Range)
        //        {
        //            if (result == null)
        //            {
        //                result = item;
        //                continue;
        //            }
        //            if (item.Y < result.Y)
        //            {
        //                result = item;
        //                continue;
        //            }

        //        }
        //        return result;
        //    }
        //}
    
    }


    internal class TSA_Plot_Coord {

        public DateTime Time { get; private set; }
        public double Hour { get; private set; }
        public double Comp { get; private set; }
        public double OT50 { get; private set; }
        public double OT100 { get; private set; }
        public double V { get; private set; }

        public TSA_Plot_Coord(DateTime time, double hour, 
            double comp=0.0, double ot50=0.0, double ot100 = 0.0, double vac = 0.0)
        {
            this.Time = time;
            this.Hour = hour;
            this.Comp = comp;
            this.OT50 = ot50;
            this.OT100 = ot100;
            this.V = vac;

        }

    }

}
