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
    public partial class OneBook : Form
    {
        public string file_name = "Books.xml";
        public string file_path;
        public Book book_data;
        public Author author_data;
        string HebBTitle, ISBN, Year, SourceLang, Translator;

        public OneBook(string HebBTitle, string ISBN, string Year, string SourceLang, string Translator)
        {
            InitializeComponent();
            file_path = Environment.CurrentDirectory + "\\" + file_name;
            this.HebBTitle = HebBTitle;
            this.ISBN = ISBN;
            this.Year = Year;
            this.SourceLang = SourceLang;
            this.Translator = Translator;
        }

        private void OneBook_Load(object sender, EventArgs e)
        {
            MinimumSize = Size;
            MaximumSize = Size;
            XDocument doc = XDocument.Load(file_path);
            bool exists = false;
            foreach (XElement book in doc.Element("data").Element("books").Nodes())
            {
                if (
                    book.Attribute("HebBTitle").Value == HebBTitle &&
                    book.Attribute("ISBN").Value == ISBN &&
                    book.Attribute("Year").Value == Year &&
                    book.Attribute("SourceLang").Value == SourceLang &&
                    book.Attribute("Translator").Value == Translator
                )
                {
                    book_data = new Book(
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
                    );
                    exists = true;
                    break;
                }
            }
            if (exists)
            {
                bool author_exists = false;
                foreach (XElement author in doc.Element("data").Element("authors").Nodes())
                {
                    if (
                        author.Attribute("HebASurname").Value == book_data.HebASurname &&
                        author.Attribute("HebAFirstname").Value == book_data.HebAFirstname
                    )
                    {
                        author_data = new Author(
                            author.Attribute("HebASurname").Value,
                            author.Attribute("HebAFirstname").Value,
                            author.Attribute("MiddleName").Value,
                            author.Attribute("Surname").Value,
                            author.Attribute("Firstname").Value
                        );
                        author_exists = true;
                        break;
                    }
                }
                if (!author_exists)
                {
                    MessageBox.Show("הסופר לא נמצא");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("הספר לא נמצא");
                Close();
            }


            label2.Text += book_data.HebBTitle != "" ? book_data.HebBTitle : "לא מוגדר";
            label3.Text += book_data.HebBSubTitle != "" ? book_data.HebBSubTitle : "לא מוגדר";
            label4.Text += book_data.ISBN != "" ? book_data.ISBN : "לא מוגדר";
            label5.Text += book_data.OriginaBlTitle != "" ? book_data.OriginaBlTitle : "לא מוגדר";
            label6.Text += book_data.OriginaBlSubTitle != "" ? book_data.OriginaBlSubTitle : "לא מוגדר";
            label7.Text += book_data.Year != "" ? book_data.Year : "לא מוגדר";
            label8.Text += book_data.SourceLang != "" ? book_data.SourceLang : "לא מוגדר";
            label9.Text += book_data.Translator != "" ? book_data.Translator : "לא מוגדר";

            label10.Text += author_data.HebAFirstname != "" ? author_data.HebAFirstname : "לא מוגדר";
            label11.Text += author_data.HebASurname != "" ? author_data.HebASurname : "לא מוגדר";
            label12.Text += author_data.MiddleName != "" ? author_data.MiddleName : "לא מוגדר";
            label13.Text += author_data.Firstname != "" ? author_data.Firstname : "לא מוגדר";
            label14.Text += author_data.Surname != "" ? author_data.Surname : "לא מוגדר";
        }
    }
}
