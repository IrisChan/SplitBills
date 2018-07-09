using System;
using System.Collections.Generic;
using SplitBills.Helpers;
using SplitBills.Models;

namespace SplitBills.Services
{
    public class SplitBillsService
    {
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;
        private readonly IExpensesCalculator _calculator;
        public IList<Trip> Trips { get; private set; }

        public SplitBillsService(IFileReader reader, IFileWriter writer, IExpensesCalculator calculator)
        {
            _fileReader = reader;
            _fileWriter = writer;
            _calculator = calculator;
        }

        public void Serve()
        {
            Console.WriteLine("Please enter fileName with the absolute path.");
            string filePath = Console.ReadLine();

            if (String.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("File name is invalid.");
                return;
            }

            bool isSuccess = SplitBills(filePath);

            if (isSuccess && Trips != null)
            {
                WriteToFile(filePath);
            }
        }

        public bool SplitBills(string filePath)
        {
            if (!_fileReader.IsValidFilePath(filePath))
            {
                Console.WriteLine("The file path or file name is invalid.");
                return false;
            }

            Trips = _fileReader.ReadFromFile(filePath);

            if (Trips == null)
            {
                Console.WriteLine("From SplitBills service: no trip is specified in the file.");
                return true;
            }

            _calculator.ComputeExpenses(Trips);

            return true;          
        }

        private void WriteToFile(string filePath)
        {
            _fileWriter.WriteToFile(filePath, Trips);
        }
    }
}
