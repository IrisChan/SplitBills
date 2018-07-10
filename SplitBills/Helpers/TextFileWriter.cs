using System;
using System.Collections.Generic;
using System.IO;
using SplitBills.Models;

namespace SplitBills.Helpers
{
    /// <summary>
    /// Text file writer: Write the split bills for each participant to file.
    /// </summary>
    public class TextFileWriter : IFileWriter
    {
        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="trips">Trips</param>
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

        /// <summary>
        /// Writes one trip.
        /// </summary>
        /// <param name="trip">Trip</param>
        /// <param name="sw">Stream Writer</param>
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
