using System;
using System.Collections.Generic;

namespace App
{
    internal class ReportMaker
    {
        public int ReportDay { get; set; }
        public List<string[]> Rows { get; set; }
        public ReportMaker(int reportDay, List<string[]> rows)
        {
            ReportDay = reportDay;
            Rows = rows;
        }

        public bool IsOkLoan(string[] line)
        {
            if (line == null || line.Length != 6)
                return false;

            if (string.IsNullOrEmpty(line[0]) || string.IsNullOrEmpty(line[1]) ||
                string.IsNullOrEmpty(line[2]) || string.IsNullOrEmpty(line[3]) ||
                string.IsNullOrEmpty(line[4]))
                return false;

            if (!int.TryParse(line[0], out _) || !int.TryParse(line[4], out int loanDay))
                return false;

            if (line[2] != "student" && line[2] != "teacher" && line[2] != "guest")
                return false;

            if (line[5] != string.Empty &&
                (!int.TryParse(line[5], out int returnDay) || returnDay < loanDay))
                return false;

            return true;
        }

        public int FineForStudent(string[] loan)
        {
            int overdueDays = Math.Max(0, EndDay(loan) - int.Parse(loan[4]) - 14);
            if (overdueDays <= 1)
                return 0;

            return Math.Min(30, overdueDays);
        }

        public int FineForLoan(string[] loan)
        {
            if (loan[2] == "student") return FineForStudent(loan);
            if (loan[2] == "teacher") return 0;
            return FineForGuest(loan);
        }

        public bool IsFlagged(string[] loan)
        {
            return loan[2] == "teacher" && string.IsNullOrEmpty(loan[5]) &&
                   ReportDay - int.Parse(loan[4]) > 60;
        }

        private int FineForGuest(string[] loan)
        {
            int overdueDays = Math.Max(0, EndDay(loan) - int.Parse(loan[4]) - 7);
            return (overdueDays * 5 + 1) / 2;
        }

        private int EndDay(string[] loan)
        {
            return string.IsNullOrEmpty(loan[5]) ? ReportDay : int.Parse(loan[5]);
        }

    }


}
