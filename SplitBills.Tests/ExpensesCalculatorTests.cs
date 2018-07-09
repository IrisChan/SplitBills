using System;
using System.Collections.Generic;
using SplitBills.Helpers;
using SplitBills.Models;
using SplitBills.Tests.Resources;
using Xunit;

namespace SplitBills.Tests
{
    public class ExpensesCalculatorTests
    {
        private readonly IExpensesCalculator _calculator = null;
        public ExpensesCalculatorTests()
        {
            _calculator = new ExpensesCalculator();
        }

        [Theory, ClassData(typeof(ExpensesCalculatorTestData))]
        public void ComputeExpensesOwnedByPerson_ShouldCalculateExpensesOwnedPerPersonInOneTrip(Trip trip, int id, decimal expected)
        {
            decimal amount = _calculator.GetExpensesOwnedByPerson(trip, id);

            Assert.Equal(amount, expected);
        }

    }
}
