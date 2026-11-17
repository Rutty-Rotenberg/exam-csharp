using System;
using System.Collections.Generic;
using System.Text;

namespace App.src
{
    public enum EnumStatus
    {
        Student, Teacher, Guest
    }
    public class Reader
    {
        public int Reader_id { get; init; }
        public int Name { get; set; }
        public int Fine { get; set; }
        public EnumStatus Status { get; set; }
        public List<Loan> Loans { get; set; }=new List<Loan>();

        public Reader(int Reader_id, int Name, int Fine, EnumStatus Status, List<Loan> Loans)
        {
            this.Reader_id = Reader_id;
            this.Name = Name;
            this.Fine = Fine;
            this.Status = Status;
            this.Loans = Loans;
        }
    }
}
