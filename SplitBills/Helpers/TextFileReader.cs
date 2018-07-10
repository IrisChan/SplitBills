using System;
using System.Collections.Generic;
using System.IO;
using SplitBills.Models;

namespace SplitBills.Helpers
{
    /// <summary>
    /// Text file reader: read file with the following format to list of trips,
    /// The information for each trip consists of a line containing a positive 
    /// integer, n, the number of peopling participating in the camping trip, 
    /// followed by n groups of inputs, one for each camping participant.  Each 
    /// groups consists of a line containing a positive integer, p, the number 
    /// of receipts/charges for that participant, followed by p lines of input, 
    /// each containing the amount, in dollars and cents, for each charge by that 
    /// camping participant. A single line containing 0 follows the information 
    /// for the last camping trip.
    /// </summary>
    public class TextFileReader : IFileReader
    {
        /// <summary>
        /// Reads from file.
        /// </summary>
        /// <returns>List of trips with expenses spent by each participant in each trip.</returns>
        /// <param name="filePath">File path.</param>
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

        /// <summary>
        /// Read one trip info from file.
        /// </summary>
        /// <returns>Trip</returns>
        /// <param name="numberParticipents">Number participents.</param>
        /// <param name="sr">Stream Reader</param>
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

        /// <summary>
        /// Gets the transactions for one participant in one trip.
        /// </summary>
        /// <param name="participentID">Participent identifier.</param>
        /// <param name="numberTransaction">Number transaction.</param>
        /// <param name="sr">Stream Reader</param>
        /// <param name="trip">Trip</param>
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

        /// <summary>
        /// Is the end of file.
        /// </summary>
        /// <returns><c>true</c>, if end of file is reached, <c>false</c> otherwise.</returns>
        /// <param name="line">Line.</param>
        private bool IsEndOfFile(string line)
        {
            return line.Equals("0");
        }

        /// <summary>
        /// Validate the file path.
        /// </summary>
        /// <returns><c>true</c>, if valid file path was valid, <c>false</c> otherwise.</returns>
        /// <param name="filePath">File path</param>
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
