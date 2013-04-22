using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TimeTrack
{
    static class Program
    {
        public static IList<TimeRecord> TimeRecords = new List<TimeRecord>();
        public static string TimeTrackerDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TimeTracker");
        public static string TimeRecordFileName = Path.Combine(TimeTrackerDir, "records.txt");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoadTimeRecords();
            Application.Run(new Form1());
            SaveTimeRecords();
        }

        private static void LoadTimeRecords()
        {
            try
            {
                if (!File.Exists(TimeRecordFileName))
                    return;
                var rx = new Regex("^(.*?)#(.*)$");
                using (var f = new StreamReader(TimeRecordFileName))
                {
                    while (!f.EndOfStream)
                    {
                        var m = rx.Match(f.ReadLine());
                        TimeRecords.Add(new TimeRecord{When = DateTime.Parse(m.Groups[1].Captures[0].Value), What = m.Groups[2].Captures[0].Value});
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Error loading time records from {0}: {1}", TimeRecordFileName, e.Message));
            }
        }

        private static void SaveTimeRecords()
        {
            try
            {
                Directory.CreateDirectory(TimeTrackerDir);
                using (var f = new StreamWriter(TimeRecordFileName, true))
                {
                    foreach (var r in TimeRecords)
                    {
                        f.WriteLine("{0}#{1}", r.When, r.What);
                    }
                }
            }
            catch (Exception e)
            {
                if (
                    MessageBox.Show(string.Format("Error writing {0}: {1}; try again?", TimeRecordFileName, e.Message), "Save Problem",
                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    SaveTimeRecords();
            }
        }
    }

    public class TimeRecord
    {
        public DateTime When;
        public string What;
    }
}
