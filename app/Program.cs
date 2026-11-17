using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using App;

// The exam program. It gets the path of an input file, and prints the report.
// Run it from the exam folder:  python check/run_checks.py run
// Without a path, it reads data/sample.txt.
class Program
{
    static void Main(string[] args)
    {
        // Hebrew is printed as UTF-8, also on Windows
        Console.OutputEncoding = new UTF8Encoding(false);
        string path = args.Length > 0 ? args[0] : FindSample();
        string[] lines = File.ReadAllLines(path, Encoding.UTF8);
        int reportDay = ReadReportDay(lines);
        List<string[]> rows = ReadRows(lines);
        List<string> rawRows = ReadRawRows(lines);

        ReportMaker report = new ReportMaker(reportDay, rows);
        List<string> errors = new List<string>();
        Dictionary<int, string> readerNames = new Dictionary<int, string>();
        Dictionary<int, int> readerFines = new Dictionary<int, int>();
        Dictionary<int, int> positiveFineLoans = new Dictionary<int, int>();
        Dictionary<string, int> bookCounts = new Dictionary<string, int>();
        List<string> flagged = new List<string>();
        HashSet<int> flaggedReaders = new HashSet<int>();
        int total = 0;

        for (int i = 0; i < rows.Count; i++)
        {
            string[] row = rows[i];
            if (!report.IsOkLoan(row))
            {
                errors.Add(rawRows[i]);
                continue;
            }

            int fine = report.FineForLoan(row);
            int readerId = int.Parse(row[0]);
            if (!readerNames.ContainsKey(readerId))
                readerNames[readerId] = row[1];
            if (!readerFines.ContainsKey(readerId))
                readerFines[readerId] = 0;
            readerFines[readerId] += fine;
            if (fine > 0)
            {
                if (!positiveFineLoans.ContainsKey(readerId))
                    positiveFineLoans[readerId] = 0;
                positiveFineLoans[readerId]++;
            }

            if (!bookCounts.ContainsKey(row[3]))
                bookCounts[row[3]] = 0;
            bookCounts[row[3]]++;

            total += fine;

            if (report.IsFlagged(row) && flaggedReaders.Add(readerId))
                flagged.Add(readerId + "," + row[1]);
        }

        Console.WriteLine("FINES");
        foreach (KeyValuePair<int, int> reader in readerFines)
            Console.WriteLine(reader.Key + "," + readerNames[reader.Key] + "," + reader.Value);
        Console.WriteLine("FLAGGED");
        foreach (string line in flagged) Console.WriteLine(line);
        Console.WriteLine("ERRORS");
        foreach (string line in errors) Console.WriteLine(line);
        Console.WriteLine("TOTAL," + total);
        Console.WriteLine("TOP_BOOKS");
        foreach (KeyValuePair<string, int> book in bookCounts
            .OrderByDescending(item => item.Value)
            .ThenBy(item => item.Key, StringComparer.Ordinal)
            .Take(3))
            Console.WriteLine(book.Key + "," + book.Value);
        Console.WriteLine("AT_RISK");
        foreach (KeyValuePair<int, int> reader in positiveFineLoans)
        {
            if (reader.Value > 2)
                Console.WriteLine(reader.Key + "," + readerNames[reader.Key]);
        }
    }

    public static int ReadReportDay(string[] lines)
    {
        return int.Parse(lines[0].Split(':')[1].Trim());
    }

    // The lines after the two first ones, each split into its fields
    static List<string[]> ReadRows(string[] lines)
    {
        List<string[]> rows = new List<string[]>();
        for (int i = 2; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;
            string[] fields = lines[i].Split(',');
            for (int j = 0; j < fields.Length; j++) fields[j] = fields[j].Trim();
            rows.Add(fields);
        }
        return rows;
    }

    static List<string> ReadRawRows(string[] lines)
    {
        List<string> rows = new List<string>();
        for (int i = 2; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;
            rows.Add(lines[i]);
        }
        return rows;
    }

    // Without a path, look for data/sample.txt next to the project, also when started from Visual Studio
    static string FindSample()
    {
        DirectoryInfo dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            string candidate = Path.Combine(dir.FullName, "data", "sample.txt");
            if (File.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        return Path.Combine("data", "sample.txt");
    }
}
