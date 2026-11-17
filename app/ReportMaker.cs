using System;
using System.Collections.Generic;
using System.Text;

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
        //public bool IsOkLoan(string[] line)
        //{
        //    for (int i = 0; i < line.Length-1; i++) 
        //    {
        //       // *שורה שאין בה בדיוק שישה שדות,
        //       // *שורה שיש בה שדה ריק חוץ מיום ההחזרה,
        //        if (line[i]==string.Empty)
        //            return false;

        //    }
        //    }
        //    /*
        //     * שורה שיש בה יום שאינו מספר,
        //     * שורה שיום ההחזרה בה לפני יום ההשאלה,
        //     * ושורה שסוג החברה בה לא מוכר.
        //     */
        //}

    }
}
