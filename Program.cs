using System;
using System.Windows.Forms;

namespace TimeTrack
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Model.LoadTimeRecords();
            Model.AddAppTimeRecord("[TimeTrack started]");
            Application.Run(new Form1());
            Model.AddAppTimeRecord("[TimeTrack stopped]");
            Model.SaveTimeRecords();
        }
    }
}
