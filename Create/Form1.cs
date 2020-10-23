using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Create
{
    public partial class Form1 : Form
    {
        private readonly FolderBrowserDialog open = new FolderBrowserDialog();
        private string path;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = open.ShowDialog();
            if (result == DialogResult.OK) path = open.SelectedPath;
            create(Convert.ToInt32(textBox1.Text));
        }

        private void create(int count)
        {
            for (var i = 0; i < count; i++)
                try
                {
                    // Create the file, or overwrite if the file exists.
                    using (var fs = File.Create(path + "\\spam" + i + fileType()))
                    {
                        var info = new UTF8Encoding(true).GetBytes(input());
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }

                    // Open the stream and read it back.
                    using (var sr = File.OpenText(path))
                    {
                        var s = "";
                        while ((s = sr.ReadLine()) != null) Console.WriteLine(s);
                    }
                }


                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
        }

        public string input()
        {
            var input = "Just some spam text. Say hellou :D";
            if (textBox2.Text != "")
                input = textBox2.Text;
            return input;
        }

        public string fileType()
        {
            var input = ".xD";
            if (textBox3.Text != "")
                input = textBox3.Text;
            return input;
        }
    }
}