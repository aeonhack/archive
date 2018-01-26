using System;
using System.IO;
using System.Windows.Forms;

namespace Protector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog T = new OpenFileDialog())
            {
                T.Filter = ".Net Executable|*.exe";
                if (T.ShowDialog() == DialogResult.OK)
                    textBox1.Text = T.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                try
                {
                    using (FileStream T = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Write))
                    {
                        if (checkBox1.Checked)
                            File.Copy(textBox1.Text, Path.ChangeExtension(textBox1.Text, ".bak"));
                        T.Seek(244, SeekOrigin.Begin);
                        T.WriteByte(10);
                        T.Close();
                    }
                    textBox1.Clear();
                    MessageBox.Show("The file has been processed successfully!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception)
                {
                    MessageBox.Show("There was an error during processing, do you have permissions to access this file or is it in use?", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a file to protect first.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.elitevs.net");
        }
    }
}
