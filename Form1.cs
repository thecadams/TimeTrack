using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TimeTrack
{
    public partial class Form1 : Form
    {
        private Timer _timer;
        private readonly KeyboardHook _hook = new KeyboardHook();
        public Form1()
        {
            InitializeComponent();
            StartTimer();
            RegisterHotKey();
        }

        private void RegisterHotKey()
        {
            _hook.KeyPressed += HookOnKeyPressed;
            _hook.RegisterHotKey(KeyModifier.Win, Keys.Z);
        }

        private void HookOnKeyPressed(object sender, KeyPressedEventArgs keyPressedEventArgs)
        {
            Show();
        }

        private void StartTimer()
        {
            _timer = new Timer {Interval = (int) TimeSpan.FromMinutes(15).TotalMilliseconds};
            _timer.Tick += (sender, args) => ShowInactiveTopmost(this);
            _timer.Start();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void ResetTimer()
        {
            _timer.Stop();
            _timer.Start();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (_timer != null)
                _timer.Stop();
            _timer = null;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Focus();
                return;
            }
            Model.AddTimeRecord(new TimeRecord{When = DateTime.Now, What = textBox1.Text});
            textBox1.Text = "";
            Visible = false;
            ResetTimer();
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTimeRecords();
        }

        private void ShowTimeRecords()
        {
            new TimeRecordsForm().Show();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        #region ShowInactiveTopMost
        private const int SW_SHOWNOACTIVATE = 4;
        private const int HWND_TOPMOST = -1;
        private const uint SWP_NOACTIVATE = 0x0010;

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
             int hWnd,           // window handle
             int hWndInsertAfter,    // placement-order handle
             int X,          // horizontal position
             int Y,          // vertical position
             int cx,         // width
             int cy,         // height
             uint uFlags);       // window positioning flags

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static void ShowInactiveTopmost(Form frm)
        {
            ShowWindow(frm.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(frm.Handle.ToInt32(), HWND_TOPMOST,
            frm.Left, frm.Top, frm.Width, frm.Height,
            SWP_NOACTIVATE);
        }
        #endregion

        private void Form1_Activated(object sender, EventArgs e)
        {
            textBox1.Focus();
            FlashEvery(10000);
        }

        private void FlashEvery(int milliseconds)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                while (true)
                {
                    Thread.Sleep(milliseconds);
                    if (!Visible) return;
                    Invoke(new MethodInvoker(() =>
                        {
                            if (!textBox1.Focused && !button1.Focused)
                                FlashWindow();
                        }));
                }
            });
        }

        #region FlashWindow
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        private void FlashWindow()
        {
            FlashWindow(Handle, true);
        }
        #endregion
    }
}
