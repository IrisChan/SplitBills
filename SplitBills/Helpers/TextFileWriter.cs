using System;
using System.Collections.Generic;
using System.IO;
using SplitBills.Models;

namespace SplitBills.Helpers
{
    public class TextFileWriter : IFileWriter
    {
        public void WriteToFile(string filePath, IList<Trip> trips)
        {
            if (trips == null)
            {
                Console.WriteLine("No trip is read from file.");
                return;
            }

            var path = filePath + ".out";
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Trip trip in trips)
                {
                    WriteOneTrip(trip, sw);
                    sw.WriteLine("");
                }
            }
        }

        private void WriteOneTrip(Trip trip, StreamWriter sw)
        {
            for (var i = 0; i < trip.NumberOfParticipents; i ++)
            {
                decimal amount = trip.Participants[i].ExpensesShouldBePaid;

                if (amount >= 0)
                {
                    sw.WriteLine("$" + amount.ToString());    
                }
                else
                {
                    sw.WriteLine(string.Format("(${0})", Math.Abs(amount).ToString()));
                }
            }
        }
    }
}
