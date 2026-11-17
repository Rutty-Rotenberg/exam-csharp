using System;
using System.Collections.Generic;
using System.Text;

namespace App.src
{
    public class Book
    {
        public int Book_Id { get; set; }
        public string Book_Name { get; set; }
        public Book(int Book_Id, string Book_Name)
        {
            this.Book_Id = Book_Id;
            this.Book_Name = Book_Name;
        }

    }
}
