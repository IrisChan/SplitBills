using System;
using SplitBills.Helpers;
using SplitBills.Services;

namespace SplitBills
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileReader fileReader = new TextFileReader();
            IFileWriter fileWriter = new TextFileWriter();
            IExpensesCalculator calculator = new ExpensesCalculator();

            IService service = new SplitBillsService(fileReader, fileWriter, calculator);
            service.Serve();
        }
    }
}
