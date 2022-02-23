using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

// SOLID Principle: Single Responsibility
namespace DesignPatterns.SolidPrinciple
{
    public class Journal
    {
        private readonly List<string> _entries = new List<string>();
        private int _count = 0;

        public int AddEntry(string entry)
        {
            _entries.Add($"{++_count}: {entry}");
            return _count;
        }

        public void RemoveEntry(int index)
        {
            _entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _entries);
        }
    }

    public class Persistence
    {
        public static void Save(string fileName, Journal journal)
        {
            File.WriteAllText(fileName, journal.ToString());
        }
    }

    public class Loader
    {
        public static Journal Load(string fileName)
        {
            Journal journal = new Journal();
            foreach (string line in GetFileContents(fileName))
            {
                journal.AddEntry(line);
            }

            return journal;
        }

        public static string[] GetFileContents(string fileName)
        {
            string fileContent = File.ReadAllText(fileName);
            return Regex.
                Replace(fileContent, "[0-9]+:\\s", "").
                Split(Environment.NewLine);
        }

        public void Load(Uri URI)
        {

        }
    }

    public class JournalDemo
    {
        public static void Run()
        {
            Journal journal = new Journal();
            journal.AddEntry("I was at the gym today.");
            journal.AddEntry("I saw her today.");
            journal.AddEntry("Reading was so much fun today.");
            journal.AddEntry("Went shopping with family.");
            journal.AddEntry("Went cycling with family.");

            Console.WriteLine($"Created Journal:\n{journal}");

            Console.WriteLine("\nSaving Journal...");
            string fileName = @"journal-20-02-2022.txt";
            Persistence.Save(fileName, journal);
            Console.WriteLine($"Successfully saved Journal to {fileName}");

            Console.WriteLine("\nLoading Journal...");
            Journal loadedJournal = Loader.Load(fileName);
            Console.WriteLine("Successfully loaded Journal file {0} into Journal object.\nHere are the Journal's entries:\n{1}", fileName, loadedJournal);
        }
    }
}
