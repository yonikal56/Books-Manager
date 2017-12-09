using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using Books;
using Classes;

namespace Books
{
    public partial class Main : Form
    {
        public string file_name = "Books.xml";
        public string file_path;

        public Main()
        {
            InitializeComponent();
            file_path = Environment.CurrentDirectory + "\\" + file_name;
            setup();
        }

        public void setup()
        {
            if (!File.Exists(file_path))
            {
                XDocument doc = new XDocument(new XElement("data", new XElement("authors", ""), new XElement("books", ""), new XElement("publishers", "")));
                doc.Save(file_path);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximumSize = Size;
            MinimumSize = Size;
            setup();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add form = new Add();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Search form = new Search();
            form.Show();
        }
    }
}
