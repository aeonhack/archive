using System;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.ComponentModel;

namespace Data_Compare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region " File Browsing "

        private string selectPath()
        {
            using (OpenFileDialog t = new OpenFileDialog())
            {
                if (t.ShowDialog() == DialogResult.OK)
                {
                    return t.FileName;
                }
                return null;
            }
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            string t = selectPath();
            if (t != null)
                textBox1.Text = t;
        }
        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            string t = selectPath();
            if (t != null)
                textBox2.Text = t;
        }

        #endregion

        #region " Begin Processing "

        byte[] file1, file2;
        long current, maximum;
        AsyncOperation O;
        DateTime start;

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Enabled = false;
            label3.Text = "Processing data..";
            label4.Visible = true;
            label4.ForeColor = Color.Red;
            progressBar1.Visible = true;

            file1 = File.ReadAllBytes(textBox1.Text);
            file2 = File.ReadAllBytes(textBox2.Text);

            maximum = file1.LongLength + file2.LongLength;
            current = maximum;

            O = AsyncOperationManager.CreateOperation(null);
            Thread t = new Thread(new ThreadStart(Scan));
            t.Start();
            start = DateTime.Now;
        }

        #endregion

        #region " Multi-Threading "

        private void Scan()
        {
            Array t1, t2;
            SendOrPostCallback rd = new SendOrPostCallback(ref Report);
            for (byte i = 0; i < 255; i++)
            {
                t1 = file1.Where(v => v == i).ToArray();
                t2 = file2.Where(v => v == i).ToArray();
                current -= Math.Abs(t1.LongLength - t2.LongLength);
                O.Post(rd, i);
            }

            //We must handle 255 here, since the loop only processes 0-254
            t1 = file1.Where(v => v == 255).ToArray();
            t2 = file2.Where(v => v == 255).ToArray();
            current -= Math.Abs(t1.LongLength - t2.LongLength);
            O.Post(rd, (byte)255);

            O.PostOperationCompleted(new SendOrPostCallback(ref Complete), null);
        }

        #endregion

        #region " UI Callbacks "

        private void Report(object i)
        {
            int t = (int)(byte)i;
            progressBar1.Value = t;
            label4.Text = string.Format("{0}%", Math.Round((double)current / (double)maximum * 100, 0).ToString());
        }
        private void Complete(object unused)
        {
            label3.Text = "Operation complete.";
            label4.ForeColor = Color.LimeGreen;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            button1.Enabled = true;
        }

        #endregion
    }
}
