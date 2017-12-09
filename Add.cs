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
    public partial class Add : Form
    {
        public string file_name = "Books.xml";
        public string file_path;
        public List<Author> authors_array = new List<Author>();

        public Add()
        {
            InitializeComponent();
            file_path = Environment.CurrentDirectory + "\\" + file_name;
        }

        public void load_authors()
        {
            BHAFN.Items.Clear();
            authors_array.Clear();
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

            foreach(Author auth in authors_array)
            {
                BHAFN.Items.Add(auth.HebAFirstname);
            }
        }

        private void Add_Load(object sender, EventArgs e)
        {
            MaximumSize = Size;
            MinimumSize = Size;
            load_authors();
        }

        private void AddAuthor_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument doc = XDocument.Load(file_path);
                XElement author = new XElement("author");
                author.SetAttributeValue("HebASurname", AHASN.Text);
                author.SetAttributeValue("HebAFirstname", AHAFN.Text);
                author.SetAttributeValue("MiddleName", AAMN.Text);
                author.SetAttributeValue("Surname", AASN.Text);
                author.SetAttributeValue("Firstname", AAFN.Text);
                doc.Root.Element("authors").Add(author);
                doc.Save(file_path);
                load_authors();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Close();
                Application.Exit();
            }
            MessageBox.Show("הסופר נוסף בהצלחה", "הודעת מידע", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            AHASN.Text = "";
            AHAFN.Text = "";
            AAMN.Text = "";
            AASN.Text = "";
            AAFN.Text = "";
        }

        private void AddBook_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument doc = XDocument.Load(file_path);
                XElement book = new XElement("book");
                book.SetAttributeValue("HebBTitle", BHBT.Text);
                book.SetAttributeValue("HebBSubTitle", BHBST.Text);
                book.SetAttributeValue("HebASurname", BHASN.Text);
                book.SetAttributeValue("HebAFirstname", BHAFN.Text);
                book.SetAttributeValue("ISBN", BBISBN.Text);
                book.SetAttributeValue("OriginaBlTitle", BBT.Text);
                book.SetAttributeValue("OriginaBlSubTitle", BBST.Text);
                book.SetAttributeValue("Year", BBY.Text);
                book.SetAttributeValue("SourceLang", BBSL.Text);
                book.SetAttributeValue("Translator", BBTR.Text);
                doc.Root.Element("books").Add(book);
                doc.Save(file_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Close();
                Application.Exit();
            }
            MessageBox.Show("הספר נוסף בהצלחה", "הודעת מידע", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            BHBT.Text = "";
            BHBST.Text = "";
            BHASN.SelectedIndex = -1;
            BHAFN.SelectedIndex = -1;
            BBISBN.Text = "";
            BBT.Text = "";
            BBST.Text = "";
            BBY.Text = "";
            BBSL.Text = "";
            BBTR.Text = "";
        }

        private void BHAFN_SelectedIndexChanged(object sender, EventArgs e)
        {
            BHASN.Items.Clear();
            if (BHAFN.SelectedIndex >= 0)
            {
                string first_name = BHAFN.SelectedItem.ToString();
                foreach (Author auth in authors_array)
                {
                    if (first_name == auth.HebAFirstname)
                    {
                        BHASN.Items.Add(auth.HebASurname);
                    }
                }
            }
        }
    }
}
