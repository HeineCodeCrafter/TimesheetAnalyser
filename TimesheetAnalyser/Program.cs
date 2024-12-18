// See https://aka.ms/new-console-template for more information
using OfficeOpenXml;
using ScottPlot;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using TimesheetAnalyser.Models;
using ScottPlot;


 
// Example data 


ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

string pattern = @"^TSN\d{6}$";
Console.ResetColor();
// Ask user for the directory path
Console.Clear();
Console.WriteLine("I will devour all *.xlsx in this folder that follows formating matching: TSNnnnnnn");
Console.Write("Please enter the directory path: "); 
string directoryPath = Console.ReadLine();

Console.Write("Print all timesheets? Y/N:\t");
string printAll = Console.ReadLine();
bool overtidePrintAll = false;
if (printAll.ToLower()=="y")
{
    overtidePrintAll = true;
}

double compTimeOffset = 0.0;


Console.WriteLine("Example:\n" +
    "41 - hour comp. time\n" +
    "31.07 - No summarization if time is exceeding normal working hours\n\n");

Console.Write("Comp.Time offset ");
string compTimeOffset_input = Console.ReadLine();
Double.TryParse(compTimeOffset_input, out compTimeOffset);

DateTime dateTime = DateTime.MinValue;
string compTimeOffsetDueTime_input = "";
if (compTimeOffset != 0)
{// 
     
    Console.Write("Comp.Time Due Time (does not summarize comp.time before this date)\n Format: DD.MM\nYour Comp.time due date: ");
    compTimeOffsetDueTime_input = Console.ReadLine();
    //DateTime dateTime = DateTime.Now;
    DateTime.TryParse(compTimeOffsetDueTime_input, out dateTime);
}


Console.Write("Type in exclude date limit. 01.12 ignores all entries from (including) 01.12:\t");
string lastMonth = Console.ReadLine();
DateTime excludeDate = DateTime.MaxValue;

DateTime.TryParse(lastMonth, out excludeDate);



// Check if the directory exists
if (!Directory.Exists(directoryPath))
{
    Console.WriteLine("Directory does not exist. Please check the path and try again.");
    return;
}

// Find all .xlsx files in the directory
string[] filePaths = Directory.GetFiles(directoryPath, "*.xlsx");

List<TSA_Day> days = new List<TSA_Day>(); 

int year = DateTime.Now.Year; // You can adjust the year if needed, here we're assuming the current year

foreach (string filePath in filePaths)
{
    string fileName = Path.GetFileNameWithoutExtension(filePath);

    if (!Regex.IsMatch(fileName, pattern))
    {
        Console.WriteLine($"Yuck! The Gastronomic experience for {fileName} does not meet the meet minimum standard for cuisine and nutrition. Propetin powder used: {pattern}");
        Console.WriteLine($"Expected a michelin 4-starr naming formation like: TSNnnnnnn.xlsx");
        continue;
    }

    Console.WriteLine($"Yummy! {fileName} Looks delicious.");

    using (var package = new ExcelPackage(new FileInfo(filePath)))
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
                            Console.WriteLine($"Failed to properly cook the A5 wagyu beef the time given: {hour_data}");
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

                        TSA_Time currentDay = new TSA_Time
                        {
                            Project = pro,
                            Hours = hours,
                            Day = header_data_dayName,
                            Category = worksheet.Cells[row, 7].Text,
                            LineProperty = worksheet.Cells[row, worksheet.Dimension.End.Column].Text
                        };
                          
                        TSA_Day? existingDay = days.FirstOrDefault(day => day.TimeStamp.Date == datetime);
                          
                        if (existingDay != null)
                        { 
                            Console.WriteLine($" ..appended to {existingDay.TimeStamp:dd.MM.yyyy}.");
                            existingDay.FoundIn = fileName;
                            existingDay.TimeRegister.Add(currentDay);
                        }
                        else
                        {
                            TSA_Day day = new TSA_Day();
                            day.TimeStamp = datetime;
                            day.FoundIn = fileName;
                            day.TimeRegister.Add(currentDay);

                            Console.WriteLine($"Added {day.TimeStamp:dd.MM.yyyy}.");
                            days.Add(day); 
                        } 
                        break; 
                    default:
                        continue;
                }
            }

            Console.WriteLine($"{fileName} tasted good."); 
        }
         
    }

    Console.WriteLine($"I've devoured on {fileName}.");
}


days = days.OrderBy(day => day.TimeStamp).ToList();

Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("Output Ready");
Console.WriteLine("...");


double totalCompTime = 0.0;
double totalOvertimeS = 0.0;
double totalOvertimeQ = 0.0;
double totalVacation = 0.0;

bool compTimeSummary = false;


foreach (TSA_Day item in days)
{
    //if (!compTimeSummary)
    //{
    //    compTimeSummary = true;
    //    Console.WriteLine($"Starting summery of daily CompTime. Total: {totalCompTime} (totalCompTime)");
    //}
    double comptime = item.GetHourSumForType(E_LinePropertyFilter.CompTime);
    double overtimeS = item.GetHourSumForType(E_LinePropertyFilter.OvertimeS);
    double overtimeQ = item.GetHourSumForType(E_LinePropertyFilter.OvertimeQ);
    double vacationDay = item.GetHourSumForType(E_LinePropertyFilter.Vacation);

    DayOfWeek dow = item.TimeStamp.DayOfWeek;

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

    if (excludeDate <= item.TimeStamp)
    {

        Console.WriteLine($"{item.TimeStamp.ToString("dd.MM.yyy"),-15}{item.FoundIn,-10}{dow.ToString(),-15} \t\tEXCLUDED: Is after {excludeDate.ToString("dd.MM.yyy")} ");
        continue;
    }

    bool printEntries = false;
    if (comptime != 0.0)
    {
        if (dateTime < item.TimeStamp)
        {

            totalCompTime += comptime;
            printEntries = true;
        } 
    }
    if (overtimeS != 0.0)
    {
        totalOvertimeS += overtimeS;
        printEntries = true;
    }
    if (overtimeQ != 0.0)
    {
        totalOvertimeQ += overtimeQ;
        printEntries = true;
    }
    if (vacationDay != 0.0)
    {
        totalVacation += vacationDay;
        printEntries = true;
    }
    if (printEntries || overtidePrintAll)
    {

        Console.Write($"{item.TimeStamp.ToString("dd.MM.yyy"),-15}{item.FoundIn,-10}{dow.ToString(),-15}");
        Console.Write($"{item.TotalHours,-5} ");
        Console.WriteLine($"entries: {item.TimeRegister.Count,-25}");
         
        Console.WriteLine($"\tComp.time: {comptime}\t\tVacation: {vacationDay} days\t\ttotalCompTime: {totalCompTime}");
          

        foreach (var t in item.TimeRegister)
        {
            Console.ResetColor();

            if (t.ToMuch)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine($"\t{t.Project.PID,-25} {t.Hours,-5} {t.Project.PName,-65} ( {t.LineProperty})");


            Console.ResetColor();
        }
    }
    //Console.WriteLine(item.TimeStamp);
}

Console.ResetColor();
Console.WriteLine("");
Console.WriteLine("Summary");
Console.WriteLine($"Comp.Time : {totalCompTime} - (summarized only for compTime AFTER {dateTime})");
Console.WriteLine($"Comp.Time Total : {totalCompTime + compTimeOffset} (added in comptime {compTimeOffset})");
Console.WriteLine($"Overtime  50% : {totalOvertimeS}");
Console.WriteLine($"Overtime 100% : {totalOvertimeQ}");
Console.WriteLine($"Vacation days : {totalVacation}");


// Get the desktop path
string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string export_filePath = Path.Combine(desktopPath, "output_day.csv");

// Write to CSV file
using (StreamWriter writer = new StreamWriter(export_filePath))
{
    // Write CSV headers
    writer.WriteLine("DateTime;FoundIn;TotalHours;LineProperty;Project");

    foreach (TSA_Day item in days)
    {
        foreach (var t in item.TimeRegister)
        { 
            writer.WriteLine($"{item.TimeStamp:dd.MM.yyyy};{item.FoundIn};{t.Hours};{t.LineProperty};{t.Project.PID} {t.Project.PName}");
        }
        //writer.WriteLine($"{item.TimeStamp:dd.MM.yyyy},{item.FoundIn},{dow},{item.TotalHours},{comptime},{overtimeS},{overtimeQ},{vacationDay},{pName}");
    }
}


Console.ReadLine();

 