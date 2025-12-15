using Newtonsoft.Json;
using System;
using System.IO; 

namespace VisualizedTimeSheets.Models.Helper
{
    internal class TSA_Settings
    {

        public static DateTime Comptime_OFS_Due_Date
        {
            get
            {
                return comptime_OFS_Due_Date;
            }

            set
            {
                comptime_OFS_Due_Date = value;
                Save();
            }
        }
        public static decimal Comptime_OFS
        {
            get
            {
                return comptime_OFS;
            }

            set
            {
                comptime_OFS = value;
                Save();
            }
        }


        private static readonly string FolderPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "VisualizedTimeSheets"
            );

        private static readonly string FilePath =
            Path.Combine(FolderPath, "tsa_settings.json");

        private static DateTime comptime_OFS_Due_Date;
        private static decimal comptime_OFS;

        public static void Save()
        {

            Directory.CreateDirectory(FolderPath);

            var data = new SettingsData
            {
                Comptime_OFS_Due_Date = Comptime_OFS_Due_Date,
                Comptime_OFS = Comptime_OFS
            };

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(FilePath, json);
            //var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            //File.WriteAllText(FilePath, json);
        }

        public static void Load()
        {
            if (!File.Exists(FilePath))
            {

                Comptime_OFS_Due_Date = DateTime.Now;
                Comptime_OFS = decimal.Zero;
                return;
            }

            var json = File.ReadAllText(FilePath);
            var data = JsonConvert.DeserializeObject<SettingsData>(json);

            if (data == null)
            {

                Comptime_OFS_Due_Date = DateTime.Now;
                Comptime_OFS = decimal.Zero;
                return;
            }

            Comptime_OFS_Due_Date = data.Comptime_OFS_Due_Date;
            Comptime_OFS = data.Comptime_OFS;
        }

        private class SettingsData
        {
            public DateTime Comptime_OFS_Due_Date { get; set; }
            public decimal Comptime_OFS { get; set; }
        }
    }
}
