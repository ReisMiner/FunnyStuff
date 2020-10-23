using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace ZipGUI
{
    public partial class Form1 : Form
    {
        private string path;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var open = new FolderBrowserDialog();
            if (open.ShowDialog() == DialogResult.OK) path = open.SelectedPath;
            using (var zip = ZipFile.Open(path + "\\first" + 0 + ".zip", ZipArchiveMode.Create))
            {
            }

            try
            {
                for (var i = 1; i < Convert.ToInt32(textBox1.Text); i++)
                {
                    using (var zip = ZipFile.Open(path + "\\first" + i + ".zip", ZipArchiveMode.Update))
                    {
                        zip.CreateEntryFromFile(path + "\\first" + (i - 1) + ".zip", i + ".zip",
                            CompressionLevel.NoCompression);
                    }

                    File.Delete(path + "\\first" + (i - 1) + ".zip");
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                throw;
            }
        }
    }
}