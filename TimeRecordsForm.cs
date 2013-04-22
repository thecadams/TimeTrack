using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace TimeTrack
{
    public partial class TimeRecordsForm : Form
    {
        public TimeRecordsForm()
        {
            InitializeComponent();
        }

        private void TimeRecordsForm_Shown(object sender, System.EventArgs e)
        {
            var data = Program.TimeRecords.Select(r => new
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
        }
    }
}
