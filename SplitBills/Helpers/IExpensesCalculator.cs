using System;
using System.Collections.Generic;
using SplitBills.Models;

namespace SplitBills.Helpers
{
    public interface IExpensesCalculator
    {
        decimal GetExpensesOwnedByPerson(Trip trip, int id);
        void ComputeExpenses(IList<Trip> trips);
    }
}
