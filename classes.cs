using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes
{
    public class Author
    {
        public string HebASurname { get; set; }
        public string HebAFirstname { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }

        public Author(string HebASurnamep, string HebAFirstnamep, string MiddleNamep, string Surnamep, string Firstnamep)
        {
            this.HebASurname = HebASurnamep;
            this.HebAFirstname = HebAFirstnamep;
            this.MiddleName = MiddleNamep;
            this.Surname = Surnamep;
            this.Firstname = Firstnamep;
        }
    }

    public class Book
    {
        public string HebBTitle { get; set; }
        public string HebBSubTitle { get; set; }
        public string HebASurname { get; set; }
        public string HebAFirstname { get; set; }
        public string ISBN { get; set; }
        public string OriginaBlTitle { get; set; }
        public string OriginaBlSubTitle { get; set; }
        public string Year { get; set; }
        public string SourceLang { get; set; }
        public string Translator { get; set; }

        public Book(string HebBTitlep, string HebBSubTitlep, string HebASurnamep, string HebAFirstnamep, string ISBNp, string OriginaBlTitlep, string OriginaBlSubTitlep, string Yearp, string SourceLangp, string Translatorp)
        {
            this.HebBTitle = HebBTitlep;
            this.HebBSubTitle = HebBSubTitlep;
            this.HebASurname = HebASurnamep;
            this.HebAFirstname = HebAFirstnamep;
            this.ISBN = ISBNp;
            this.OriginaBlTitle = OriginaBlTitlep;
            this.OriginaBlSubTitle = OriginaBlSubTitlep;
            this.Year = Yearp;
            this.SourceLang = SourceLangp;
            this.Translator = Translatorp;
        }
    }
}
