using System;
using SplitBills.Models;
using Xunit;

namespace SplitBills.Tests
{
    public class ParticipantTests
    {
        [Fact]
        public void ExpensesPaidAlready_ShouldReturnTotalExpensesPaidByThatPersonInOneTrip()
        {
            // Arrange
            Participant participant = new Participant(1);

            // Act
            participant.Expenses.Add(3.0m);
            participant.Expenses.Add(2.1m);
            participant.Expenses.Add(6.12m);

            // Assert
            Assert.Equal(11.22m, participant.ExpensesPaidAlready);
        }

        [Fact]
        public void ExpensesPaidAlready_ShouldReturnZeroWhenThatPersonDoesNotPayAnyExpenses()
        {
            // Arrange
            Participant participant = new Participant(1);

            // Assert
            Assert.Equal(0m, participant.ExpensesPaidAlready);
        }
    }
}
