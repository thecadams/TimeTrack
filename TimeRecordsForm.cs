using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TimeTrack
{
    public partial class TimeRecordsForm : Form
    {
        private DateTime CurrentDate = DateTime.Now;

        public TimeRecordsForm()
        {
            InitializeComponent();
        }

        private void TimeRecordsForm_Shown(object sender, System.EventArgs e)
        {
            var data = Program.TimeRecords.OrderByDescending(r => r.When).Select(r => new
                {
                    Date = r.When.Date.ToShortDateString(),
                    Time = r.When.TimeOfDay.ToString().Substring(0, 8),
                    r.What
                });
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.AddRange(data.Select(r =>
                {
                    var row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewTextBoxCell {Value = r.Date});
                    row.Cells.Add(new DataGridViewTextBoxCell {Value = r.Time});
                    row.Cells.Add(new DataGridViewTextBoxCell {Value = r.What});
                    return row;
                }).ToArray());
            dataGridView1.Refresh();

            DrawDay();
        }

        private void DrawDay()
        {
            var row = Array.FindIndex(Program.TimeRecords.OrderByDescending(r => r.When).ToArray(), r => r.When.Date == CurrentDate.Date);
            if (row >= 0)
                dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[0];
            lblCurrentDay.Text = CurrentDate.ToString("dddd MMMM d, yyyy");
            var dateOfCurrentButton = CurrentDate.StartOfWeek();
            foreach (var btn in new[] {btnMonday, btnTuesday, btnWednesday, btnThursday, btnFriday, btnSaturday, btnSunday})
            {
                var countForDay = Program.TimeRecords.CountForDay(dateOfCurrentButton);
                btn.Text = string.Format(btn.Tag.ToString().Replace("\\n", Environment.NewLine), dateOfCurrentButton.ToString("dd/MM/yyyy"), countForDay);
                btn.Enabled = countForDay != 0;
                dateOfCurrentButton = dateOfCurrentButton.AddDays(1);
            }
            var g = pnlPoints.CreateGraphics();
            g.Clear(SystemColors.Control);

            var todayRecords = Program.TimeRecords.Where(r => r.When.Date == CurrentDate.Date).OrderBy(r => r.When).ToList();
            if (!todayRecords.Any())
            {
                pnlPoints.VerticalScroll.Value = 0;
                vScrollBar1.Enabled = false;
                return;
            }
            var minTicks = (double)todayRecords.First().When.Ticks;
            var maxTicks = (double)todayRecords.Last().When.Ticks;
            const int padding = 10;
            const int radius = 10;
            const int minX = padding + radius;
            var maxX = pnlPoints.Width - vScrollBar1.Width - radius - padding;
            var y = padding + radius - vScrollBar1.Value;
            const int yStep = padding + (2*radius);

            var circleBrush = Brushes.DarkBlue;
            var font = new Font("Arial", 8.0F);
            var brush = Brushes.Black;
            foreach (var r in todayRecords)
            {
                var what = string.Format("{0}: {1}", r.When.ToString("HH:mm:ss"), r.What);
                // need to calculate a point between minX and maxX according to the Ticks value between minTicks and maxTicks
                // eg. if ticks are 10...100 and x is 1...5, a point "50" on the tick scale is at 3.222
                // this is calculated by working out where the tick is from a zero origin (ie. 40/90) along the 1...5 scale.
                // formula: ((40/90)*4)+1
                // or: ((T-minT)/(maxT-minT))*(maxX-minX) + minX
                var x = (int)Math.Round((((r.When.Ticks - minTicks)/(maxTicks - minTicks))*(maxX-minX)) + minX);
                g.FillEllipse(circleBrush, x, y, radius, radius);
                // Decide whether to draw the text on the LHS or RHS of the red dot
                var textSize = TextRenderer.MeasureText(what, font);
                var lhsTextX = x - radius - textSize.Width;
                var rhsTextX = x + radius;
                var textIsBetterOnLhs = rhsTextX + textSize.Width > maxX && lhsTextX > minX;
                var textX = textIsBetterOnLhs ? lhsTextX : rhsTextX;
                var textY = y - 2;
                g.DrawString(what, font, brush, textX, textY);
                y += yStep;
            }

            var vScrollMax = y + radius + padding;
            vScrollBar1.Enabled = vScrollMax > pnlPoints.Height;
            pnlPoints.VerticalScroll.Value = vScrollBar1.Value;
        }

        private void btnPrevWeek_Click(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.AddDays(-7);
            DrawDay();
        }

        private void btnMonday_Click(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.DaysSinceStartOfWeek(0);
            DrawDay();
        }

        private void btnTuesday_Click(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.DaysSinceStartOfWeek(1);
            DrawDay();
        }

        private void btnWednesday_Click(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.DaysSinceStartOfWeek(2);
            DrawDay();
        }

        private void btnThursday_Click(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.DaysSinceStartOfWeek(3);
            DrawDay();
        }

        private void btnFriday_Click(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.DaysSinceStartOfWeek(4);
            DrawDay();
        }

        private void btnSaturday_Click(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.DaysSinceStartOfWeek(5);
            DrawDay();
        }

        private void btnSunday_Click(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.DaysSinceStartOfWeek(6);
            DrawDay();
        }

        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.AddDays(7);
            DrawDay();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            DrawDay();
        }
    }

    public static class MyExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Sunday)
                return dt.Date.AddDays(-6); // go back to start
            return dt.Date.AddDays(1 - (int)dt.DayOfWeek);
        }

        public static DateTime DaysSinceStartOfWeek(this DateTime dt, int days)
        {
            return dt.StartOfWeek().AddDays(days);
        }

        public static int CountForDay(this IEnumerable<TimeRecord> records, DateTime dt)
        {
            return records.Count(r => r.When.Date == dt.Date);
        }
    }
}
