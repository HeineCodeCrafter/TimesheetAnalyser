using BrightIdeasSoftware;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using ScottPlot;
using ScottPlot.TickGenerators.Financial;
using ScottPlot.TickGenerators.TimeUnits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualizedTimeSheets.Models;
using VisualizedTimeSheets.Models.Helper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Color = System.Drawing.Color;

namespace VisualizedTimeSheets.Views
{
    public partial class frm_dashboard : Form
    {
        List<TSA_Day> Days = new List<TSA_Day>();
        List<TSA_Day> ReportDays = new List<TSA_Day>();
        List<TSA_Overview> OverviewSummary = new List<TSA_Overview>();

        double Report_TimeSummary_Comp = 0.0;
        double Report_TimeSummary_OvertimeS = 0.0;
        double Report_TimeSummary_OvertimeQ = 0.0;
        double Report_TimeSummary_Vacation = 0.0;

        //string data_directory = @"C:\Programmering\repo\HeineCodeCrafter\workfolder\Timesheets";
        
        // Privat
        //string data_directory = @"C:\Users\tom25\OneDrive\Dokumenter\Workfolder\TimeSheetAnalyser\Timesheets";
        
        // Jobb PC
        string data_directory = @"C:\Users\tom.saether\OneDrive - Nordomatic Group\Dokumenter\Workfolder\TimeSheetAnalyser\Timesheets";

        public frm_dashboard()
        {
            InitializeComponent();
            TSA_Settings.Load();

            dtp_comp_time_due_date.Value = TSA_Settings.Comptime_OFS_Due_Date;
            nud_offset_comptime.Value = TSA_Settings.Comptime_OFS;

            txt_dataDirectory.Text = data_directory;

            Visualization_Formation();

            

            ImportData(data_directory);

             

            // Get the first day of the last month
            dtp_report_start.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);

            // Get the last day of the last month
            dtp_report_end.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);

        }

        private void UpdateDataVisualisation()
        {
            //olv_imported.SetObjects(Days);
            olv_day_entries.Update();

            olv_imported.Update();
            if (olv_imported.SelectedIndex < 0)
            {
                olv_imported.SelectedIndex = 0;
            }

            olv_report_days.Update();
            olv_report_days.AutoSizeColumns();
            if (olv_report_days.SelectedIndex < 0)
            {
                olv_report_days.SelectedIndex = 0;
            }
             
            olv_report_selcted_index.Update();
            olv_report_selcted_index.AutoSizeColumns();
             
            olv_timesheet_overview.Update();
            olv_timesheet_overview.AutoSizeColumns();

        }

        private void Visualization_Formation()
        {

            #region Time Sheets 

            olv_imported.AllColumns.Clear();

            olv_imported.ShowGroups = false;
            olv_imported.FullRowSelect = true;
            olv_imported.MultiSelect = false; 
            olv_imported.HideSelection = false;
            olv_imported.HeaderUsesThemes = false;
            olv_imported.EmptyListMsg = $"Listen er tom..";
            olv_imported.AllColumns.Add(new OLVColumn() { Text = "Date", MinimumWidth = 75, AspectName = "Date", ToolTipText = "Dato" });
            olv_imported.AllColumns.Add(new OLVColumn() { Text = "DayOfWeek", MinimumWidth = 15, AspectName = "DayOfWeek", ToolTipText = "DayOfWeek" });
            olv_imported.AllColumns.Add(new OLVColumn() { Text = "TotalHours", MinimumWidth = 20, AspectName = "TotalHours", ToolTipText = "Total hours registered" });
            //olv_imported.AllColumns.Add(new OLVColumn() { Text = "OT 50%", MinimumWidth = 20, AspectName = "SumOvertimeS", ToolTipText = "SumOvertimeS" });
            //olv_imported.AllColumns.Add(new OLVColumn() { Text = "OT 100%", MinimumWidth = 20, AspectName = "SumOvertimeQ", ToolTipText = "SumOvertimeQ" });
            //olv_imported.AllColumns.Add(new OLVColumn() { Text = "Ferie", MinimumWidth = 20, AspectName = "SumVacation", ToolTipText = "SumVacation" });
            olv_imported.AllColumns.Add(new OLVColumn() { Text = "Comp.", MinimumWidth = 20, AspectName = "SumCompTime", ToolTipText = "SumCompTime" });
            olv_imported.AllColumns.Add(new OLVColumn() { Text = "FoundIn", MinimumWidth = 75, AspectName = "FoundIn", ToolTipText = "Found in TimeSheet", FillsFreeSpace = true});
            //olv_imported.AllColumns.Add(new OLVColumn() { Text = "Tagnavn", MinimumWidth = 150,  AspectName = "ComponentName", ToolTipText = "Systemnummer/oppdeling" });
            //olv_imported.AllColumns.Add(new OLVColumn() { Text = "System", MinimumWidth = 150, AspectName = "SystemFull", ToolTipText = "Systemnummer/oppdeling" });
            //olv_imported.AllColumns.Add(new OLVColumn() { Text = "Datatype", Width = 75, AspectName = "DataType", ToolTipText = "Data Point Type i ETS" });
            //olv_imported.AllColumns.Add(new OLVColumn() { Text = "Funksjonskode", MinimumWidth = 70, AspectName = "HVAC_FunctionCode", ToolTipText = "Funksjonsbeskrivelse referanse" });
            //olv_imported.AllColumns.Add(new OLVColumn() { Text = "Ekskluder", AspectName = "Exclude", CheckBoxes = true, ToolTipText = "Ekskluder signal, enten grunnet ukjent tagnavn eller manglende SD-merke" });

            olv_imported.PrimarySortOrder = SortOrder.Descending;
            olv_imported.PrimarySortColumn = olv_imported.GetColumn("Date");
            olv_imported.RebuildColumns();

            olv_imported.SetObjects(Days);


            olv_day_entries.AllColumns.Clear(); 
            olv_day_entries.ShowGroups = false;
            olv_day_entries.FullRowSelect = true;
            olv_day_entries.MultiSelect = false;
            olv_day_entries.HideSelection = false;
            olv_day_entries.HeaderUsesThemes = false;
            olv_day_entries.EmptyListMsg = $"Velg en dag i menyen til venstre";
            olv_day_entries.AllColumns.Add(new OLVColumn() { Text = "Day", MinimumWidth = 15, AspectName = "Day", ToolTipText = "Day" });
            olv_day_entries.AllColumns.Add(new OLVColumn() { Text = "Hours", MinimumWidth = 15, AspectName = "Hours", ToolTipText = "Hours" });
            olv_day_entries.AllColumns.Add(new OLVColumn() { Text = "Category", MinimumWidth = 15, AspectName = "Category", ToolTipText = "Category" });
            olv_day_entries.AllColumns.Add(new OLVColumn() { Text = "LineProperty", MinimumWidth = 20, AspectName = "LineProperty", ToolTipText = "LineProperty" });
            olv_day_entries.AllColumns.Add(new OLVColumn() { Text = "ToMuch", CheckBoxes=true, MinimumWidth = 10, AspectName = "ToMuch", ToolTipText = "ToMuch" });
            olv_day_entries.AllColumns.Add(new OLVColumn() { Text = "IsWeekend", CheckBoxes = true, MinimumWidth = 10, AspectName = "IsWeekend", ToolTipText = "IsWeekend" });
            olv_day_entries.AllColumns.Add(new OLVColumn() { Text = "Project", MinimumWidth = 50, AspectName = "Project", ToolTipText = "Project", FillsFreeSpace = true });
            olv_day_entries.PrimarySortOrder = SortOrder.Descending;
            olv_day_entries.PrimarySortColumn = olv_imported.GetColumn("Day");
            olv_day_entries.RebuildColumns();
             
            olv_day_entries.FormatRow += delegate (object sender, FormatRowEventArgs e)
            {
                if (e.Model is TSA_Time)
                {
                    TSA_Time x = (TSA_Time)e.Model;
                    if (x.ToMuch || x.IsWeekend)
                    { 
                        e.Item.BackColor = Color.FromArgb(192, 57, 43);
                        e.Item.ForeColor = Color.Black; 
                    } 
                    else
                    {
                        e.Item.BackColor = Color.White;
                        e.Item.ForeColor = Color.Black;
                    }
                }
            };

            #endregion

            #region Report

            olv_report_days.AllColumns.Clear(); 

            olv_report_days.ShowGroups = false;
            olv_report_days.FullRowSelect = true;
            olv_report_days.MultiSelect = false;
            olv_report_days.HideSelection = false;
            olv_report_days.HeaderUsesThemes = false;
            olv_report_days.EmptyListMsg = $"Listen er tom..";
            olv_report_days.AllColumns.Add(new OLVColumn() { Text = "Date", MinimumWidth = 75, AspectName = "Date", ToolTipText = "Dato" });
            olv_report_days.AllColumns.Add(new OLVColumn() { Text = "DayOfWeek", MinimumWidth = 15, AspectName = "DayOfWeek", ToolTipText = "DayOfWeek" });
            olv_report_days.AllColumns.Add(new OLVColumn() { Text = "TotalHours", MinimumWidth = 20, AspectName = "TotalHours", ToolTipText = "Total hours registered" });
            olv_report_days.AllColumns.Add(new OLVColumn() { Text = "Comp.", MinimumWidth = 20, AspectName = "SumCompTime", ToolTipText = "SumCompTime" });
            olv_report_days.AllColumns.Add(new OLVColumn() { Text = "OT50", MinimumWidth = 15, AspectName = "SumOvertimeS", ToolTipText = "SumCompTime" });
            olv_report_days.AllColumns.Add(new OLVColumn() { Text = "OT100.", MinimumWidth = 15, AspectName = "SumOvertimeQ", ToolTipText = "SumCompTime" });
            olv_report_days.AllColumns.Add(new OLVColumn() { Text = "Vac.", MinimumWidth = 10, AspectName = "SumVacation", ToolTipText = "SumCompTime" });
            olv_report_days.AllColumns.Add(new OLVColumn() { Text = "FoundIn", MinimumWidth = 75, AspectName = "FoundIn", ToolTipText = "Found in TimeSheet", FillsFreeSpace = true });
            olv_report_days.PrimarySortColumn = olv_imported.GetColumn("Date");
            olv_report_days.RebuildColumns();
            

            olv_report_selcted_index.AllColumns.Clear();
            olv_report_selcted_index.ShowGroups = false;
            olv_report_selcted_index.FullRowSelect = true;
            olv_report_selcted_index.MultiSelect = false;
            olv_report_selcted_index.HideSelection = false;
            olv_report_selcted_index.HeaderUsesThemes = false;
            olv_report_selcted_index.EmptyListMsg = $"Velg en dag i menyen til venstre";
            olv_report_selcted_index.AllColumns.Add(new OLVColumn() { Text = "Day", MinimumWidth = 15, AspectName = "Day", ToolTipText = "Day" });
            olv_report_selcted_index.AllColumns.Add(new OLVColumn() { Text = "Hours", MinimumWidth = 12, AspectName = "Hours", ToolTipText = "Hours" });
            olv_report_selcted_index.AllColumns.Add(new OLVColumn() { Text = "LineProperty", MinimumWidth = 20, AspectName = "LineProperty", ToolTipText = "LineProperty" });
            olv_report_selcted_index.AllColumns.Add(new OLVColumn() { Text = "ToMuch", CheckBoxes = true, MinimumWidth = 10, AspectName = "ToMuch", ToolTipText = "ToMuch" });
            olv_report_selcted_index.AllColumns.Add(new OLVColumn() { Text = "IsWeekend", CheckBoxes = true, MinimumWidth = 10, AspectName = "IsWeekend", ToolTipText = "IsWeekend" });
            olv_report_selcted_index.AllColumns.Add(new OLVColumn() { Text = "Category", MinimumWidth = 15, AspectName = "Category", ToolTipText = "Category" });
            olv_report_selcted_index.AllColumns.Add(new OLVColumn() { Text = "Project", MinimumWidth = 50, AspectName = "Project", ToolTipText = "Project", FillsFreeSpace = true });
            olv_report_selcted_index.PrimarySortOrder = SortOrder.Descending;
            olv_report_selcted_index.PrimarySortColumn = olv_imported.GetColumn("Day");
            olv_report_selcted_index.RebuildColumns();

            olv_report_selcted_index.FormatRow += delegate (object sender, FormatRowEventArgs e)
            {
                if (e.Model is TSA_Time)
                {
                    TSA_Time x = (TSA_Time)e.Model;
                    if (x.ToMuch || x.IsWeekend)
                    {
                        e.Item.BackColor = Color.FromArgb(192, 57, 43);
                        e.Item.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Item.BackColor = Color.White;
                        e.Item.ForeColor = Color.Black;
                    }
                }
            };

            #endregion

            #region Time Sheet Overview
             
            olv_timesheet_overview.AllColumns.Clear();

            olv_timesheet_overview.ShowGroups = false;
            olv_timesheet_overview.FullRowSelect = true;
            olv_timesheet_overview.MultiSelect = false;
            olv_timesheet_overview.HideSelection = false;
            olv_timesheet_overview.HeaderUsesThemes = false;
            olv_timesheet_overview.EmptyListMsg = $"Listen er tom..";
            olv_timesheet_overview.AllColumns.Add(new OLVColumn() { Text = "TSN", MinimumWidth = 120, AspectName = "TSN", ToolTipText = "TSN" }); 
            olv_timesheet_overview.AllColumns.Add(new OLVColumn() { Text = "In", MinimumWidth = 10, CheckBoxes=true, AspectName = "Imported", ToolTipText = "Imported" });
            olv_timesheet_overview.AllColumns.Add(new OLVColumn() { Text = "Total", MinimumWidth = 75, AspectName = "Total", ToolTipText = "Total hours", FillsFreeSpace = true });
            olv_timesheet_overview.PrimarySortColumn = olv_timesheet_overview.GetColumn("TSN");
            

            olv_timesheet_overview.RebuildColumns();
            #endregion
        }

        #region GUI Events

        #endregion
        private void olv_imported_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void olv_imported_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void olv_imported_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = olv_imported.SelectedObject;
            if (selected is TSA_Day)
            {
                TSA_Day day = (TSA_Day)selected;
                olv_day_entries.SetObjects(day.TimeRegister); 
                UpdateDataVisualisation();
            }
        }

        private void Show_Details(TSA_Day sender)
        { 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data_directory = txt_dataDirectory.Text;
            ImportData(data_directory);
        }

        private void ImportData(string directory_path)
        {
            Days = new List<TSA_Day>();
            List<string> directories = Directory.GetDirectories(directory_path).ToList();

            ImportOverview(directory_path);

            //Console.WriteLine("Subfolders:");
            foreach (string dir in directories)
            {
                string directory_year = Path.GetFileNameWithoutExtension(dir);
                int year = int.Parse(directory_year); // You can adjust the year if needed, here we're assuming the current year
                string[] files = Directory.GetFiles(dir, "*.xlsx");
                foreach (var file in files)
                {
                    string pattern = @"^TSN\d{6}$";
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (!Regex.IsMatch(fileName, pattern))
                    {
                        //Console.WriteLine($"Yuck! The Gastronomic experience for {fileName} does not meet the meet minimum standard for cuisine and nutrition. Propetin powder used: {pattern}");
                        //Console.WriteLine($"Expected a michelin 4-starr naming formation like: TSNnnnnnn.xlsx");
                        continue;
                    } 
                 
                    using (var package = new ExcelPackage(new FileInfo(file)))
                    {
                        try
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming the data is in the first sheet

                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {

                                for (int i = 8; i < worksheet.Dimension.End.Column; i++)
                                {

                                    string header_data = worksheet.Cells[1, i].Text;
                                    string header_data_dayName = header_data.Split(' ').FirstOrDefault();
                                    switch (header_data_dayName.ToLower())
                                    {
                                        case "mon":
                                        case "tue":
                                        case "wed":
                                        case "thu":
                                        case "fri":
                                        case "sat":
                                        case "sun":
                                            string header_data_date = header_data.Substring(header_data.IndexOf(' ') + 1);
                                            string fullDate = header_data_date + "." + year;
                                            DateTime datetime = DateTime.ParseExact(fullDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);


                                            string hour_data = worksheet.Cells[row, i].Text;
                                            double hours;
                                            if (!double.TryParse(hour_data, out hours))
                                            {
                                                //Console.WriteLine($"Failed to properly cook the A5 wagyu beef the time given: {hour_data}");
                                                continue;
                                            }
                                            if (hours == 0.0)
                                            {
                                                continue;
                                            }

                                            TSA_Project pro = new TSA_Project
                                            {
                                                Customer = worksheet.Cells[row, 1].Text,
                                                Name = worksheet.Cells[row, 2].Text,
                                                PID = worksheet.Cells[row, 3].Text,
                                                PName = worksheet.Cells[row, 4].Text,
                                                ActivityNumber = worksheet.Cells[row, 5].Text,
                                                Activity = worksheet.Cells[row, 6].Text
                                            };

                                            TSA_Time current_time = new TSA_Time
                                            {
                                                Project = pro,
                                                Hours = hours,
                                                Day = header_data_dayName,
                                                Category = worksheet.Cells[row, 7].Text,
                                                LineProperty = worksheet.Cells[row, worksheet.Dimension.End.Column].Text
                                            };

                                            TSA_Day existingDay = Days.FirstOrDefault(day => day.TimeStamp.Date == datetime);

                                            if (existingDay != null)
                                            {
                                                //Console.WriteLine($" ..appended to {existingDay.TimeStamp:dd.MM.yyyy}.");
                                                existingDay.FoundIn = fileName;
                                                existingDay.TimeRegister.Add(current_time);
                                            }
                                            else
                                            {
                                                TSA_Day day = new TSA_Day();
                                                day.TimeStamp = datetime;
                                                day.FoundIn = fileName;
                                                var tsa_overview = OverviewSummary.Find(t => t.TSN == day.FoundIn);
                                                tsa_overview.Imported = true;

                                                day.TimeRegister.Add(current_time);

                                                //Console.WriteLine($"Added {day.TimeStamp:dd.MM.yyyy}.");
                                                Days.Add(day);
                                            }
                                            break;
                                        default:
                                            continue;
                                    }
                                }

                            }

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                }
            }

            Days = Days.OrderBy(d => d.TimeStamp).ToList();

            olv_imported.SetObjects(Days);
            olv_timesheet_overview.SetObjects(OverviewSummary);
            UpdateDataVisualisation();
        }

        private void ImportOverview(string directory_path)
        {
            OverviewSummary = new List<TSA_Overview>();
            string file = Path.Combine(directory_path, $"TSN_Overview.xlsx");
              
            using (var package = new ExcelPackage(new FileInfo(file)))
            {
                try
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming the data is in the first sheet

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    { 
                        TSA_Overview tsa_overview = new TSA_Overview();
                        tsa_overview.TSN = $"{worksheet.Cells[row, 1].Text}";
                        tsa_overview.Resource = $"{worksheet.Cells[row, 2].Text}";
                        tsa_overview.PeriodStart = DateTime.Parse($"{worksheet.Cells[row, 3].Text}");
                        tsa_overview.PeriodEnd = DateTime.Parse($"{worksheet.Cells[row, 4].Text}");
                        tsa_overview.Approval = $"{worksheet.Cells[row, 5].Text}"; 
                        tsa_overview.Total = $"{worksheet.Cells[row, 6].Text}";
                        tsa_overview.Imported = false;
                        OverviewSummary.Add(tsa_overview); 

                    }

                }
                catch (Exception ignored) { }
            }
             
            olv_timesheet_overview.SetObjects(OverviewSummary);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void CreateReport()
        {
            TSA_Settings.Comptime_OFS = nud_offset_comptime.Value;
            TSA_Settings.Comptime_OFS_Due_Date = dtp_comp_time_due_date.Value;

            DateTime start = dtp_report_start.Value;
            DateTime end = dtp_report_end.Value;
            ReportDays = Days.FindAll(d => d.TimeStamp>= start && d.TimeStamp <= end).ToList();

            olv_report_days.SetObjects(ReportDays);
            VisualizeHourTypes();
            ReportSummary();

        }

        private void VisualizeHourTypes()
        {
            //List<TSA_Plot> plot_data = new List<TSA_Plot>();
            TSA_Plot plot_data = new TSA_Plot();
            foreach (var day in ReportDays)
            {
                double hours = day.TotalHours;
                double comptime = day.GetHourSumForType(E_LinePropertyFilter.CompTime);
                double overtimeS = day.GetHourSumForType(E_LinePropertyFilter.OvertimeS);
                double overtimeQ = day.GetHourSumForType(E_LinePropertyFilter.OvertimeQ);
                double vacationDay = day.GetHourSumForType(E_LinePropertyFilter.Vacation);

                TSA_Plot_Coord data_coord = new TSA_Plot_Coord(
                    day.TimeStamp,
                    hours,comptime,overtimeS,overtimeQ,vacationDay);
                
                plot_data.Append(data_coord);

                //plot_data.Add

            }


            DateTime[] x_main = plot_data.Range.Select(coord => coord.Time).ToArray();

            double[] y_hour = plot_data.Range.Select(coord => coord.Hour).ToArray();
            double[] y_comp = plot_data.Range.Select(coord => coord.Comp).ToArray();
            double[] y_ot50 = plot_data.Range.Select(coord => coord.OT50).ToArray();
            double[] y_ot100 = plot_data.Range.Select(coord => coord.OT100).ToArray();
            double[] y_vac = plot_data.Range.Select(coord => coord.V).ToArray();

              
            //time_sheet_plot.Plot.Add.Scatter(x_main, y_hour);
            //time_sheet_plot.Plot.Add.Scatter(x_main, y_comp);
           

            //time_sheet_plot.Plot.Add.Scatter(x_main, y_ot50);
            //time_sheet_plot.Plot.Add.Scatter(x_main, y_ot100);
            //time_sheet_plot.Plot.Add.Scatter(x_main, y_vac);
            //time_sheet_plot.Plot.Add.Scatter(x_main, y_hour);

            //// setup the bottom axis to use DateTime ticks
            //var axis = time_sheet_plot.Plot.Axes.DateTimeTicksBottom();

            ////double[] dataX = { 1, 2, 3, 4, 5 };
            ////double[] dataY = { 1, 4, 9, 16, 25 };
            
            ////time_sheet_plot.Plot.Add.Scatter(dataY, dataY);
            //time_sheet_plot.Refresh();
        }

        private void nud_offset_comptime_ValueChanged(object sender, EventArgs e)
        {
            CreateReport();
        }

        private void dtp_comp_time_due_date_ValueChanged(object sender, EventArgs e)
        {

            CreateReport();
        }
        private void dtp_report_start_ValueChanged(object sender, EventArgs e)
        {
            CreateReport();
        }


        private void dtp_report_end_ValueChanged(object sender, EventArgs e)
        {
            CreateReport();
        }
        private void olv_report_days_SelectedIndexChanged(object sender, EventArgs e)
        {

            var selected = olv_report_days.SelectedObject;
            if (selected is TSA_Day)
            {
                TSA_Day day = (TSA_Day)selected;
                olv_report_selcted_index.SetObjects(day.TimeRegister);
                UpdateDataVisualisation();
            }
             

        }


        private void olv_report_selcted_index_SelectedIndexChanged(object sender, EventArgs e)
        {  
        }

        private void ReportSummary()
        {
            double.TryParse($"{nud_offset_comptime.Value}",out double comp_time_ofs); 
            DateTime comp_time_ofs_due_date = dtp_comp_time_due_date.Value;
            Console.WriteLine($"");
            Console.WriteLine($"Report Summary");


            List<DateTime> data_series_time = new List<DateTime>();
            List<double> data_series_value = new List<double>(); 

            Report_TimeSummary_Comp = 0.0;
            Report_TimeSummary_OvertimeS = 0.0;
            Report_TimeSummary_OvertimeQ = 0.0;
            Report_TimeSummary_Vacation = 0.0;

            foreach (TSA_Day day in ReportDays)
            {
                //Console.Write($"{day.TimeStamp.ToString("dd.MM.yyy"),-15}{day.FoundIn,-10}{dow.ToString(),-15}");
                Console.WriteLine($"{day.Date} {day.FoundIn} {day.DayOfWeek,-15}");
                double comptime = day.GetHourSumForType(E_LinePropertyFilter.CompTime);
                double overtimeS = day.GetHourSumForType(E_LinePropertyFilter.OvertimeS);
                double overtimeQ = day.GetHourSumForType(E_LinePropertyFilter.OvertimeQ);
                double vacationDay = day.GetHourSumForType(E_LinePropertyFilter.Vacation)/7.5;

                //Report_TimeSummary_Comp += comptime;
                //Report_TimeSummary_OvertimeS += overtimeS;
                //Report_TimeSummary_OvertimeQ += overtimeQ;
                //Report_TimeSummary_Vacation += vacationDay;
               
                if (comptime != 0.0)
                {
                    if (comptime * -1 == overtimeS + overtimeQ)
                    {
                        comptime = comptime + overtimeS + overtimeQ;

                    }
                    if (comp_time_ofs_due_date < day.TimeStamp)
                    {
                        Report_TimeSummary_Comp += comptime;
                        //printEntries = true;
                    }
                }
                if (overtimeS != 0.0)
                {
                    Report_TimeSummary_OvertimeS += overtimeS;
                    //printEntries = true;
                }
                if (overtimeQ != 0.0)
                {
                    Report_TimeSummary_OvertimeQ += overtimeQ;
                    //printEntries = true;
                }
                if (vacationDay != 0.0)
                {
                    Report_TimeSummary_Vacation += vacationDay;
                    //printEntries = true;
                }
                DayOfWeek dow = day.TimeStamp.DayOfWeek;
                switch (dow)
                {
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:

                        break;
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                    default:
                        break;
                }

                //Console.Write($"{day.TimeStamp:dd.MM.yyy} {day.FoundIn,-10}{dow.ToString(),-15} Totals: | Comp {Report_TimeSummary_OvertimeS,-5} {Report_TimeSummary_Comp,-5} | OT50: {Report_TimeSummary_OvertimeS,-5} | OT100: {Report_TimeSummary_OvertimeQ,-5} | V: {Report_TimeSummary_OvertimeS,-5}");
                Console.WriteLine($"\tDaysum: | Comp {comptime,-5} | OT50: {overtimeS,-5} | OT100: {overtimeQ,-5} | V: {vacationDay,-5}");
                Console.WriteLine($"\tTotals: | Comp {Report_TimeSummary_Comp,-5} | OT50: {Report_TimeSummary_OvertimeS,-5} | OT100: {Report_TimeSummary_OvertimeQ,-5} | V: {Report_TimeSummary_OvertimeS,-5}");
                Console.WriteLine($"\tentries: {day.TimeRegister.Count,-25}");
                foreach (var t in day.TimeRegister)
                {
                    //Console.ResetColor();

                    //if (t.ToMuch)
                    //{
                    //    Console.ForegroundColor = ConsoleColor.DarkRed;
                    //}
                    Console.WriteLine($"\t\t{t.Project.PID,-25} {t.Hours,-5} {t.Project.PName,-65} ( {t.LineProperty})");


                    //Console.ResetColor();
                }


            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Summary");
            Console.WriteLine($"Comp.Time : {Report_TimeSummary_Comp}");
            Console.WriteLine($"Comp.Time Total : {Report_TimeSummary_Comp + comp_time_ofs} (added in comptime {comp_time_ofs})");
            Console.WriteLine($"Overtime  50% : {Report_TimeSummary_OvertimeS}");
            Console.WriteLine($"Overtime 100% : {Report_TimeSummary_OvertimeQ}");
            Console.WriteLine($"Vacation days : {Report_TimeSummary_Vacation}");

            lbl_report_summary.Text = 
                $"Comp.time for period: {Report_TimeSummary_Comp}\n" + 
                $"Comp.time Total : {Report_TimeSummary_Comp + comp_time_ofs} (added in comptime {comp_time_ofs})\n" +
                $"Overtime  50% : {Report_TimeSummary_OvertimeS}\n" +
                $"Overtime 100% : {Report_TimeSummary_OvertimeQ}\n" +
                $"Vacation days : {Report_TimeSummary_Vacation}\n";

        }

        private void time_sheet_plot_Load(object sender, EventArgs e)
        {

        }

        private void btn_export_current_period_Click(object sender, EventArgs e)
        {
            DateTime start = Days.LastOrDefault().TimeStamp;
            DateTime end = Days.LastOrDefault().TimeStamp;
            string filename = $"TimeSheet Analyser {DateTime.Now:yyMMdd HHmmss} export period {start:yy.MM.dd} - {end:yy.MM.dd}.csv";

            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = Path.Combine(downloadsPath, filename);

            //List<string> lines = new List<string>();
             
            using (StreamWriter writer = new StreamWriter(filePath))
            {

                writer.WriteLine($"Date;TotalHours;SumCompTime;SumOvertimeS;SumOvertimeQ;SumVacation;FoundIn");
                // Write each day's data
                foreach (TSA_Day day in ReportDays)
                {
                    List<string> lines = new List<string>();
                    lines.Add($"{day.Date}");
                    lines.Add($"{day.TotalHours}");
                    lines.Add($"{day.SumCompTime}");
                    lines.Add($"{day.SumOvertimeS}");
                    lines.Add($"{day.SumOvertimeQ}");
                    lines.Add($"{day.SumVacation}"); 
                    lines.Add($"{day.FoundIn}");
                    writer.WriteLine(string.Join(";", lines));
                }

            }

            Process.Start(filePath);
        }
    }
}
