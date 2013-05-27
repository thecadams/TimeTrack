using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TimeTrack;

namespace TimeTrack
{
    public class Model
    {
        private static readonly IList<TimeRecord> TimeRecords = new List<TimeRecord>();
        private static readonly string TimeTrackerDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TimeTracker");
        private static readonly string TimeRecordFileName = Path.Combine(TimeTrackerDir, "records.txt");
        private static readonly string TimeRecordBackupFormat = Path.Combine(TimeTrackerDir, "records.{0}.txt");

        protected static string TimeRecordBackupFileName
        {
            get { return string.Format(TimeRecordBackupFormat, DateTime.Now.ToString("yyyyMMdd.HHmmss")); }
        }

        public static void LoadTimeRecords()
        {
            try
            {
                if (!File.Exists(TimeRecordFileName))
                    return;
                var rxLegacy = new Regex("^(.*?)#(.*)$");
                var rx = new Regex("^(.*?)#(.*)#(.*)$");
                using (var f = new StreamReader(TimeRecordFileName))
                {
                    while (!f.EndOfStream)
                    {
                        var line = f.ReadLine();
                        if (line == null)
                            continue;
                        if (rx.IsMatch(line))
                        {
                            var m = rx.Match(line);
                            var when = DateTime.Parse(m.Groups[1].Captures[0].Value);
                            var kind = (TimeRecordKind)Enum.Parse(typeof(TimeRecordKind), m.Groups[2].Captures[0].Value);
                            var what = m.Groups[3].Captures[0].Value;
                            if (!TimeRecords.Any(r => r.When == when && r.What == what && r.Kind == kind))
                                TimeRecords.Add(new TimeRecord { When = when, What = what, Kind = kind });
                        }
                        else
                        {
                            var m = rxLegacy.Match(line);
                            var when = DateTime.Parse(m.Groups[1].Captures[0].Value);
                            var what = m.Groups[2].Captures[0].Value;
                            if (!TimeRecords.Any(r => r.When == when && r.What == what))
                                TimeRecords.Add(new TimeRecord {When = when, What = what});
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Error loading time records from {0}: {1}", TimeRecordFileName, e.Message));
            }
        }

        public static void SaveTimeRecords()
        {
            try
            {
                Directory.CreateDirectory(TimeTrackerDir);
                File.Copy(TimeRecordFileName, TimeRecordBackupFileName);
                using (var f = new StreamWriter(TimeRecordFileName))
                {
                    foreach (var r in TimeRecords)
                    {
                        f.WriteLine("{0}#{1}#{2}", r.When, r.Kind, r.What);
                    }
                }
            }
            catch (Exception e)
            {
                if (
                    MessageBox.Show(string.Format("Error writing {0}: {1}; try again?", TimeRecordFileName, e.Message), @"Save Problem",
                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    SaveTimeRecords();
            }
        }

        public static void AddTimeRecord(string what)
        {
            AddTimeRecord(new TimeRecord{When = DateTime.Now, What = what});
        }

        public static void AddAppTimeRecord(string what)
        {
            AddTimeRecord(new TimeRecord{When = DateTime.Now, What = what, Kind = TimeRecordKind.App});
        }

        public static void AddTimeRecord(TimeRecord r)
        {
            TimeRecords.Add(r);
            SaveSingleTimeRecord(r);
        }

        public static void SaveSingleTimeRecord(TimeRecord r)
        {
            try
            {
                using (var f = new StreamWriter(TimeRecordFileName, true))
                    f.WriteLine("{0}#{1}", r.When, r.What);
            }
            catch (Exception e)
            {
                if (
                    MessageBox.Show(string.Format("Error writing {0}: {1}; try again?", TimeRecordFileName, e.Message), @"Save Problem",
                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    SaveSingleTimeRecord(r);
            }
        }
        
        public static IList<TimeRecord> TimeRecordsForDay(DateTime dt)
        {
            return TimeRecords.Where(r => r.When.Date == dt.Date).OrderBy(r => r.When).ToList();
        }

        public static IOrderedEnumerable<TimeRecord> TimeRecordsNewestToOldest()
        {
            return TimeRecords.OrderByDescending(r => r.When);
        }

        public static int TimeRecordCountForDay(DateTime dt)
        {
            return TimeRecords.Count(r => r.When.Date == dt.Date);
        }
    }

    public enum TimeRecordKind
    {
        Normal,
        App
    }

    public class TimeRecord
    {
        public DateTime When;
        public string What;
        public TimeRecordKind Kind;
    }
}

public static class MyExtensions
{
    public static void Invoke(this Control control, MethodInvoker action)
    {
        control.Invoke(action);
    }

    public static Brush GetBrush(this TimeRecord record)
    {
        switch (record.Kind)
        {
            case TimeRecordKind.Normal:
                return Brushes.DarkBlue;
            case TimeRecordKind.App:
                return Brushes.DarkCyan;
            default:
                throw new InvalidOperationException("Unknown kind " + record.Kind);
        }
    }
}
