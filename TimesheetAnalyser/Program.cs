// See https://aka.ms/new-console-template for more information
using OfficeOpenXml;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using TimesheetAnalyser.Models;


ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

string pattern = @"^TSN\d{6}$";

// Ask user for the directory path

Console.WriteLine("I will devour all *.xlsx in this folder that follows formating matching: TSNnnnnnn");
Console.Write("Please enter the directory path: "); 
string directoryPath = Console.ReadLine();

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
                           Console.WriteLine($"Failed to properly cook the A5-beef the time given: {hour_data}");
                            continue;
                        }
                        if (hours == 0.0)
                        {
                            //Console.WriteLine($"Not interested in undercoocked food: {hours}h");
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
                            existingDay.TimeRegister.Add(currentDay);
                        }
                        else
                        {
                            TSA_Day day = new TSA_Day();
                            day.TimeStamp = datetime;

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
            //TSA_Day? existingDay = days.FirstOrDefault(day => day.Date.Date == currentDay.Date);
            //if (existingDay != null)
            //{
            //    // If the date already exists, add the time entries to the existing TimeRegister
            //    existingDay.TimeRegister.AddRange(currentDay.TimeRegister);
            //}
            //else
            //{
            //    TSA_Day day = new TSA_Day();
            //    day.TimeRegister timeRegister
            //                // If the date is not found, add the new TSA_Day to the list
            //                days.Add(datetime);
            //}

            //foreach (KeyValuePair<string, DateTime> day in dayColumns)
            //{

            //}
            //DataRow newRow = table.NewRow();
            //for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            //{


            //    //newRow[col - 1] = worksheet.Cells[row, col].Text;
            //}
            //table.Rows.Add(newRow);
        }

        //tables.Add(table);
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

foreach (TSA_Day item in days)
{
    Console.WriteLine("");

    double comptime = item.GetHourSumForType(E_LinePropertyFilter.CompTime);
    double overtimeS = item.GetHourSumForType(E_LinePropertyFilter.OvertimeS);
    double overtimeQ = item.GetHourSumForType(E_LinePropertyFilter.OvertimeQ);
    double vacationDay = item.GetHourSumForType(E_LinePropertyFilter.Vacation); 

    Console.WriteLine($"{item.TimeStamp.ToString("dd.MM.yyy"),-25} {item.TotalHours,-5} entries: {item.TimeRegister.Count,-25}");

    bool printEntries = false;
    if (comptime != 0.0)
    {
        Console.WriteLine($"\tComp.time: {comptime,-25} h");
        totalCompTime += comptime;
        printEntries = true;
    }
    if (overtimeS != 0.0)
    {
        Console.WriteLine($"\tOvertime  50%: {overtimeS,-25} h");
        totalOvertimeS += overtimeS;
        printEntries = true;
    }
    if (overtimeQ != 0.0)
    {
        Console.WriteLine($"\tOvertime 100%: {overtimeQ,-25} h");
        totalOvertimeQ += overtimeQ;
        printEntries = true;
    }
    if (vacationDay != 0.0)
    {
        Console.WriteLine($"\tVacation : {vacationDay,-25} days");
        totalVacation += vacationDay;
        printEntries = true;
    }
    if (printEntries)
    {

        foreach (var t in item.TimeRegister)
        {
            Console.WriteLine($"\t{t.Project.PID,-25} {t.Hours,-5} {t.Project.PName,-65} ( {t.LineProperty})");
        }
    }
    //Console.WriteLine(item.TimeStamp);
}

Console.WriteLine("");
Console.WriteLine("Summary");
Console.WriteLine($"Comp.Time : {totalCompTime}");
Console.WriteLine($"Overtime  50% : {totalOvertimeS}");
Console.WriteLine($"Overtime 100% : {totalOvertimeQ}");
Console.WriteLine($"Vacation days : {totalVacation}");
Console.ReadLine();

// Now 'tables' contains a list of DataTables with the data from each Excel file.
// Here, you can process or merge these tables as needed.
//foreach (var table in tables)
//{
//    PrintTable(table); // Function to print table contents to console
//}

//static void PrintTable(DataTable table)
//{
//    Console.WriteLine(string.Join("\t", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
//    foreach (DataRow row in table.Rows)
//    {
//        Console.WriteLine(string.Join("\t", row.ItemArray));
//    }
//}

//DataTable table = new DataTable();

//Dictionary<string,DateTime> dayColumns = new Dictionary<string, DateTime> ();

//// Look for headers, and create the days available in this document
//for (int header_column = 1; header_column <= worksheet.Dimension.End.Column; header_column++)
//{

//    string colum_data = worksheet.Cells[1, header_column].Text; 
//    string colum_data_dayName = colum_data.Split(' ').FirstOrDefault();
//    switch (colum_data_dayName.ToLower())
//    {
//        case "mon":
//        case "tue":
//        case "wed":
//        case "thu":
//        case "fri":
//        case "sat":
//        case "sun": 
//            string datePart = colum_data.Substring(colum_data.IndexOf(' ') + 1); 
//            string fullDate = datePart + "." + year; 
//            DateTime datetime = DateTime.ParseExact(fullDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

//            if (dayColumns.ContainsKey(colum_data_dayName))
//            {
//                Console.WriteLine($"Day already exist in dictionary: {colum_data} in {filePath}");
//                // nope, dictionary
//                continue;
//            }

//            Console.WriteLine($"Day added: {colum_data_dayName} => {datetime} from {filePath}");
//            dayColumns.Add(colum_data_dayName.ToLower(), datetime);
//            break;
//        default:
//            continue;
//    }


//}
// Work 