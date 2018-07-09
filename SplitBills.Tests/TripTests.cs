using System;
using System.Collections.Generic;
using SplitBills.Models;
using Xunit;

namespace SplitBills.Tests
{
    public class TripTests
    {
        Trip trip = null;

        public TripTests()
        {
            trip = new Trip(3);

            IList<Participant> participants = new List<Participant>();
            for (var i = 0; i < 3; i++)
            {
                participants.Add(new Participant(i));
            }

            participants[0].Expenses.Add(10.00m);
            participants[0].Expenses.Add(20.00m);

            participants[1].Expenses.Add(15.00m);
            participants[1].Expenses.Add(15.01m);
            participants[1].Expenses.Add(3.00m);
            participants[1].Expenses.Add(3.01m);

            participants[2].Expenses.Add(5.00m);
            participants[2].Expenses.Add(9.00m);
            participants[2].Expenses.Add(4.00m);

            trip.Participants = (participants);
        }

        [Fact]
        public void GetTotalExpenses_ShouldBeSumOfAllExpenses()
        {
            // Assert
            Assert.Equal(84.02m, trip.TotalExpenses);
        }

        [Fact]
        public void GetAverageExpenses_ShouldBeAverageExpensesForEachParticipant()
        {
            // Assert
            Assert.Equal(28.01m, decimal.Round(trip.AverageExpenses, 2));
        }

    }
}
