using System;
using System.Collections.Generic;
using System.Linq;

namespace SplitBills.Models
{
    public class Trip
    {
        public int NumberOfParticipents { get; set; }
        public IList<Participant> Participants { get; set; }

        public decimal TotalExpenses
        {
            get
            {
                return GetTotalExpenses();
            }
        }

        public decimal AverageExpenses
        {
            get
            {
                return GetAverageExpenses();   
            }
        }

        public Trip(int numberOfParticipents)
        {
            NumberOfParticipents = numberOfParticipents;
            Participants = new List<Participant>();
        }

        private decimal GetTotalExpenses()
        {
            return Participants.Sum(x => x.ExpensesPaidAlready);
        }

        private decimal GetAverageExpenses()
        {
            return GetTotalExpenses() / NumberOfParticipents;
        }
    }
}
