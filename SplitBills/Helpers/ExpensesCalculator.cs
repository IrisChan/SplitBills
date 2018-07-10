using System.Collections.Generic;
using SplitBills.Models;

namespace SplitBills.Helpers
{
    /// <summary>
    /// Expenses calculator to split bills.
    /// </summary>
    public class ExpensesCalculator : IExpensesCalculator
    {
        /// <summary>
        /// Gets the expenses owned by person.
        /// </summary>
        /// <returns>The expenses owned by person.</returns>
        /// <param name="trip">Trip</param>
        /// <param name="id">Participant Identifier</param>
        public decimal GetExpensesOwnedByPerson(Trip trip, int id)
        {
            decimal amount = decimal.Round(trip.AverageExpenses - trip.Participants[id].ExpensesPaidAlready, 2);

            return amount;
        }

        /// <summary>
        /// Computes the expenses for each trip.
        /// </summary>
        /// <param name="trips">Trips</param>
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
