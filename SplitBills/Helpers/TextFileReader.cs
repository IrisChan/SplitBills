using System;
using System.Collections.Generic;
using System.IO;
using SplitBills.Models;

namespace SplitBills.Helpers
{
    public class TextFileReader : IFileReader
    {
        public IList<Trip> ReadFromFile(string filePath)
        {
            if(!IsValidFilePath(filePath))
            {
                Console.WriteLine("The file path or file name is invalid.");
                return null;
            }

            IList<Trip> trips = new List<Trip>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null && !IsEndOfFile(line)) 
                {
                    int numberParticipents = Convert.ToInt32(line);
                    Trip trip = GetOneTrip(numberParticipents, sr);
                    if (trip != null)
                    {
                        trips.Add(trip);
                    }
                }
            }

            return trips;
        }

        private Trip GetOneTrip(int numberParticipents, StreamReader sr)
        {
            if (numberParticipents < 1)
            {
                return null;
            }

            Trip trip = new Trip(numberParticipents);

            string line = null;
            for (var i = 0; i < numberParticipents; i ++)
            {
                line = sr.ReadLine();
                int numberTransactions = Convert.ToInt32(line);

                trip.Participants.Add(new Participant(i));
                GetTransactions(i, numberTransactions, sr, trip);
            }

            return trip;
        }

        private void GetTransactions(int participentID, int numberTransaction, StreamReader sr, Trip trip)
        {
            if (numberTransaction < 1)
            {
                return;
            }

            string line = null;
            for (var i = 0; i < numberTransaction; i ++)
            {
                line = sr.ReadLine();
                decimal amount = Convert.ToDecimal(line);
                trip.Participants[participentID].Expenses.Add(amount);
            }
        }

        private bool IsEndOfFile(string line)
        {
            return line.Equals("0");
        }

        public bool IsValidFilePath(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                return false;
            }

            if (!filePath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) || !File.Exists(filePath))
            {
                return false;
            }

            return true;
        }
    }
}
