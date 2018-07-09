using System.Collections.Generic;
using System.IO;
using Moq;
using SplitBills.Helpers;
using SplitBills.Models;
using SplitBills.Services;
using Xunit;

namespace SplitBills.Tests
{
    public class SplitBillsServiceTests
    {
        
        [Fact]
        public void SplitBills_ShouldReturnListOfTripsThatContainsExpensesOwnedPerPerson()
        {
            string filePath = Path.GetFullPath("Resources/testfile.txt");

            decimal[] expected = new decimal[] { -1.99m, -8.01m, 10.01m, 0.98m, -0.98m };

            IFileReader fileReader = new TextFileReader();
            IFileWriter fileWriter = new TextFileWriter();
            IExpensesCalculator calculator = new ExpensesCalculator();

            SplitBillsService service = new SplitBillsService(fileReader, fileWriter, calculator);
            bool isSuccess = service.SplitBills(filePath);

            Assert.True(isSuccess);

            int i = 0;
            foreach (var trip in service.Trips)
            {
                foreach (var participant in trip.Participants)
                {
                    Assert.Equal(expected[i++], participant.ExpensesShouldBePaid);
                }
            }

        }



    }
}
