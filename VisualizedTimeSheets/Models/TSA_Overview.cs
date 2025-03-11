using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using VisualizedTimeSheets.Properties;

namespace VisualizedTimeSheets.Models
{
    internal class TSA_Overview
    { 
        public string TSN { get; set; }
        public string Resource { get; set; }
        public string Approval { get; set; }
        public string Total { get; set; }
        public bool Imported { get; set; } 
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }

        public TSA_Overview()
        {
            TSN = string.Empty;
            Resource = string.Empty;
            Approval = string.Empty;
            Total = string.Empty;
            Imported = false;
            PeriodStart = DateTime.MinValue;
            PeriodEnd = DateTime.MinValue;
             
        }
    }
}
