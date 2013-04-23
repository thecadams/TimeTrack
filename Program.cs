using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TimeTrack
{
    static class Program
    {
        public static IList<TimeRecord> TimeRecords = new List<TimeRecord>();
        public static string TimeTrackerDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TimeTracker");
        public static string TimeRecordFileName = Path.Combine(TimeTrackerDir, "records.txt");
        public static string TimeRecordBackupFormat = Path.Combine(TimeTrackerDir, "records.{0}.txt");

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
                        var when = DateTime.Parse(m.Groups[1].Captures[0].Value);
                        var what = m.Groups[2].Captures[0].Value;
                        if (!TimeRecords.Any(r => r.When == when && r.What == what))
                            TimeRecords.Add(new TimeRecord{When = when, What = what});
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
                File.Copy(TimeRecordFileName, string.Format(TimeRecordBackupFormat, DateTime.Now.ToString("yyyyMMdd.HHmmss")));
                using (var f = new StreamWriter(TimeRecordFileName))
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
