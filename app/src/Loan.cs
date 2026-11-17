using System;
using System.Collections.Generic;
using System.Text;

namespace App.src
{
    public class Loan
    {
        public int Reader_id { get; init; }
        public string ReaderName { get; set; }
        public EnumStatus Status { get; set; }
        public Book Book { get; set; }
        public DateTime loan_day { get; set; }
        public DateTime return_day { get; set; }

        public Loan(int Reader_id, string ReaderName, EnumStatus Status, Book Book, DateTime loan_day, DateTime return_day)
        {
            this.Reader_id = Reader_id;
            this.ReaderName = ReaderName;
            this.Status = Status;
            this.Book = Book;
            this.loan_day = loan_day;
            this.return_day = return_day;
        }
    }
}
