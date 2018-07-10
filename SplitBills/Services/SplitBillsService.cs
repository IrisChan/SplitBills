using System;
using System.Collections.Generic;
using SplitBills.Helpers;
using SplitBills.Models;

namespace SplitBills.Services
{
    /// <summary>
    /// 1. Read a list of trips from a file if exists, 
    /// 2. Split bills for each participant
    /// 3. Write expenses owned by each participant to file with postfix ".out"
    /// </summary>
    public class SplitBillsService : IService
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

        /// <summary>
        /// Given valid filename, compute each person's owned amount, and output the result to file.
        /// </summary>
        /// <returns>
        ///    <c>true</c>, if trips were read, bills was split, and output to a file
        ///    <c>false</c> otherwise.
        /// </returns>
        public bool Serve()
        {
            Console.WriteLine("Please enter fileName with the absolute path.");
            string filePath = Console.ReadLine();

            if (String.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("File name is invalid.");
                return false;
            }

            bool isSuccess = SplitBills(filePath);

            if (isSuccess && Trips != null)
            {
                WriteToFile(filePath);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Splits the bills.
        /// </summary>
        /// <returns>
        ///    <c>true</c>, if trips were read and bills was split, 
        ///    <c>false</c> otherwise.
        /// </returns>
        /// 
        /// <param name="filePath">File path.</param>
        public bool SplitBills(string filePath)
        {
            if (!_fileReader.IsValidFilePath(filePath))
            {
                Console.WriteLine("The file path or file name is invalid.");
                return false;
            }

            try
            {
                Trips = _fileReader.ReadFromFile(filePath);
            }
            catch(Exception)
            {
                Console.WriteLine("Unable to process input file.");
                return false;
            }


            if (Trips == null)
            {
                Console.WriteLine("From SplitBills service: no trip is specified in the file.");
                return true;
            }

            _calculator.ComputeExpenses(Trips);

            return true;          
        }

        /// <summary>
        /// Writes bills to file.
        /// </summary>
        /// <param name="filePath">File path.</param>
        private void WriteToFile(string filePath)
        {
            _fileWriter.WriteToFile(filePath, Trips);
        }
    }
}
