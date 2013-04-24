using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TimeTrack
{
    public partial class TimeRecordsForm : Form
    {
        private DateTime _currentDate = DateTime.Now;
        private DateTime CurrentDate
        {
            get { return _currentDate; }
            set
            {
                if (value == _currentDate)
                    return;
                _scrollPosition = 0;
                vScrollBar1.Value = 0;
                _currentDate = value;
            }
        }

        private int _scrollPosition;

        public TimeRecordsForm()
        {
            InitializeComponent();
        }

        private void TimeRecordsForm_Shown(object sender, EventArgs e)
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
            UpdateGrid();
            UpdateDayControls();

            var todayRecords = Program.TimeRecords.Where(r => r.When.Date == CurrentDate.Date).OrderBy(r => r.When).ToList();
            var vScrollMax = (2 * RowPadding) + (todayRecords.Count() * YStep);
            vScrollBar1.Enabled = vScrollMax > pnlPoints.Height;
            if (vScrollBar1.Enabled)
            {
                vScrollBar1.Maximum = vScrollMax;
                vScrollBar1.LargeChange = pnlPoints.Height;
            }

            if (!todayRecords.Any()) return;
            var minTicks = (double)todayRecords.First().When.Ticks;
            var maxTicks = (double)todayRecords.Last().When.Ticks;
            var maxX = pnlPoints.Width - vScrollBar1.Width - Radius - RowPadding;
            var y = RowPadding + Radius - _scrollPosition;

            using (var bg = BufferedGraphicsManager.Current.Allocate(pnlPoints.CreateGraphics(), pnlPoints.DisplayRectangle))
            {
                var g = bg.Graphics;
                g.Clear(SystemColors.Control);
                // If there's a single record just draw it at the left most position.
                if (todayRecords.Count == 1)
                    DrawRecord(todayRecords[0], g, MinX, y, maxX);
                else
                {
                    foreach (var r in todayRecords)
                    {
                        var aboveViewport = y < 0 - RowPadding - Radius;
                        var belowViewport = y > pnlPoints.Height + Radius + RowPadding;
                        if (!aboveViewport && !belowViewport)
                        {
                            // need to calculate a point between minX and maxX according to the Ticks value between minTicks and maxTicks
                            // eg. if ticks are 10...100 and x is 1...5, a point "50" on the tick scale is at 2.778
                            // this is calculated by working out where the tick is from a zero origin (ie. 40/90) along the 1...5 scale.
                            // formula: ((40/90)*4)+1
                            // or: ((T-minT)/(maxT-minT))*(maxX-minX) + minX
                            var x = (int)Math.Round((((r.When.Ticks - minTicks) / (maxTicks - minTicks)) * (maxX - MinX)) + MinX);
                            DrawRecord(r, g, x, y, maxX);
                        }
                        y += YStep;
                    }
                }
                bg.Render();
            }
        }

        private const int RowPadding = 10;
        private const int Radius = 10;
        private const int MinX = RowPadding + Radius;
        private const int YStep = RowPadding + (2 * Radius);
        private static readonly Brush CircleBrush = Brushes.DarkBlue;
        private static readonly Font TextFont = new Font("Arial", 8.0F);
        private static readonly Brush Brush = Brushes.Black;

        private static void DrawRecord(TimeRecord r, Graphics g, int x, int y, int maxX)
        {
            var what = string.Format("{0}: {1}", r.When.ToString("HH:mm:ss"), r.What);
            g.FillEllipse(CircleBrush, x, y, Radius, Radius);
            // Decide whether to draw the text on the LHS or RHS of the red dot
            var textSize = TextRenderer.MeasureText(what, TextFont);
            var lhsTextX = x - Radius - textSize.Width;
            var rhsTextX = x + Radius;
            var textIsBetterOnLhs = rhsTextX + textSize.Width > maxX && lhsTextX > MinX;
            var textX = textIsBetterOnLhs ? lhsTextX : rhsTextX;
            var textY = y - 2;
            g.DrawString(what, TextFont, Brush, textX, textY);
        }

        private void UpdateDayControls()
        {
            lblCurrentDay.Text = CurrentDate.ToString("dddd MMMM d, yyyy");
            var dateOfCurrentButton = CurrentDate.StartOfWeek();
            foreach (var btn in new[] {btnMonday, btnTuesday, btnWednesday, btnThursday, btnFriday, btnSaturday, btnSunday})
            {
                var countForDay = Program.TimeRecords.CountForDay(dateOfCurrentButton);
                btn.Text = string.Format(btn.Tag.ToString(), dateOfCurrentButton.ToString("dd/MM/yy"), countForDay);
                btn.Enabled = countForDay != 0;
                dateOfCurrentButton = dateOfCurrentButton.AddDays(1);
            }
        }

        private void UpdateGrid()
        {
            var row = Array.FindIndex(Program.TimeRecords.OrderByDescending(r => r.When).ToArray(),
                                      r => r.When.Date == CurrentDate.Date);
            if (row >= 0)
                dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[0];
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
            if (e.OldValue == e.NewValue) return;
            _scrollPosition = e.NewValue;
            DrawDay();
        }

        private void pnlPoints_Resize(object sender, EventArgs e)
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
