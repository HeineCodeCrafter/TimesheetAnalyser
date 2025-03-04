using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualizedTimeSheets.Views;

namespace VisualizedTimeSheets
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            Application.Run(new frm_dashboard());
        }
    }
}
