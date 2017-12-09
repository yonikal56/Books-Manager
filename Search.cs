using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Classes;

namespace Books
{
    public partial class Search : Form
    {
        public string file_name = "Books.xml";
        public string file_path;
        public List<Author> authors_array = new List<Author>();
        public List<Book> books_array = new List<Book>();

        public Search()
        {
            InitializeComponent();
            file_path = Environment.CurrentDirectory + "\\" + file_name;
        }

        public void load_data(string hebrew_title = "", string hebrew_author = "", string isbn = "", string year = "", string source_lang = "", string translator = "")
        {
            DataListView.Sorting = SortOrder.Ascending;
            DataListView.Items.Clear();
            authors_array.Clear();
            books_array.Clear();

            XDocument doc = XDocument.Load(file_path);
            foreach (XElement author in doc.Element("data").Element("authors").Nodes())
            {
                authors_array.Add(new Author(
                    author.Attribute("HebASurname").Value,
                    author.Attribute("HebAFirstname").Value,
                    author.Attribute("MiddleName").Value,
                    author.Attribute("Surname").Value,
                    author.Attribute("Firstname").Value
                ));
            }

            foreach (XElement book in doc.Element("data").Element("books").Nodes())
            {
                if (
                    book.Attribute("HebBTitle").Value.StartsWith(hebrew_title) &&
                    book.Attribute("ISBN").Value.StartsWith(isbn) &&
                    book.Attribute("Year").Value.StartsWith(year) &&
                    book.Attribute("SourceLang").Value.StartsWith(source_lang) &&
                    book.Attribute("Translator").Value.StartsWith(translator) &&
                    (
                        (book.Attribute("HebASurname").Value + " " + book.Attribute("HebAFirstname").Value).StartsWith(hebrew_author)
                        ||
                        (book.Attribute("HebAFirstname").Value + " " + book.Attribute("HebASurname").Value).StartsWith(hebrew_author)
                    )
                )
                {
                    books_array.Add(new Book(
                        book.Attribute("HebBTitle").Value,
                        book.Attribute("HebBSubTitle").Value,
                        book.Attribute("HebASurname").Value,
                        book.Attribute("HebAFirstname").Value,
                        book.Attribute("ISBN").Value,
                        book.Attribute("OriginaBlTitle").Value,
                        book.Attribute("OriginaBlSubTitle").Value,
                        book.Attribute("Year").Value,
                        book.Attribute("SourceLang").Value,
                        book.Attribute("Translator").Value
                    ));
                }
            }

            foreach (Book book in books_array)
            {
                ListViewItem item = new ListViewItem(book.HebBTitle);
                item.SubItems.Add(book.HebASurname + " " + book.HebAFirstname);
                item.SubItems.Add(book.ISBN);
                item.SubItems.Add(book.Year);
                item.SubItems.Add(book.SourceLang);
                item.SubItems.Add(book.Translator);
                DataListView.Items.Add(item);
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {
            MaximumSize = Size;
            MinimumSize = Size;
            load_data();
        }

        private void DataListView_DoubleClick(object sender, EventArgs e)
        {
            //open book data
            if (DataListView.SelectedItems.Count == 1)
            {
                OneBook book = new OneBook(
                    DataListView.SelectedItems[0].SubItems[0].Text,
                    DataListView.SelectedItems[0].SubItems[2].Text,
                    DataListView.SelectedItems[0].SubItems[3].Text,
                    DataListView.SelectedItems[0].SubItems[4].Text,
                    DataListView.SelectedItems[0].SubItems[5].Text);
                book.Show();
            }
        }

        private void מחיקהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataListView.SelectedItems.Count == 1)
            {
                XDocument doc = XDocument.Load(file_path);
                doc.Element("data").Element("books").Elements("book").Where(
                    elemet => (elemet.Attribute("HebBTitle").Value == DataListView.SelectedItems[0].SubItems[0].Text)
                ).Where(
                    elemet => (elemet.Attribute("ISBN").Value == DataListView.SelectedItems[0].SubItems[2].Text)
                ).Where(
                    elemet => (elemet.Attribute("Year").Value == DataListView.SelectedItems[0].SubItems[3].Text)
                ).Where(
                    elemet => (elemet.Attribute("SourceLang").Value == DataListView.SelectedItems[0].SubItems[4].Text)
                ).Where(
                    elemet => (elemet.Attribute("Translator").Value == DataListView.SelectedItems[0].SubItems[5].Text)
                )
                .ToList().ForEach(i => i.Remove());
                doc.Save(file_path);
                MessageBox.Show("נמחק בהצלחה");
                load_data(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataListView.Sorting == SortOrder.Ascending)
            {
                DataListView.Sorting = SortOrder.Descending;
            }
            else
            {
                DataListView.Sorting = SortOrder.Ascending;
            }
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            load_data(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load_data(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
    }
}
