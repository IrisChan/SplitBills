using System.Collections.Generic;
using SplitBills.Models;

namespace SplitBills.Helpers
{
    public class ExpensesCalculator : IExpensesCalculator
    {
        public decimal GetExpensesOwnedByPerson(Trip trip, int id)
        {
            decimal amount = decimal.Round(trip.AverageExpenses - trip.Participants[id].ExpensesPaidAlready, 2);

            return amount;
        }

        public void ComputeExpenses(IList<Trip> trips)
        {
            if (trips == null)
            {
                return;
            }

            foreach (var trip in trips)
            {
                for (var i = 0; i < trip.NumberOfParticipents; i ++)
                {
                    decimal amount = GetExpensesOwnedByPerson(trip, i);
                    trip.Participants[i].ExpensesShouldBePaid = amount;
                }
            }
        }
    }
}
