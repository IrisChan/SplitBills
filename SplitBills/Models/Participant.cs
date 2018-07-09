using System.Collections.Generic;
using System.Linq;

namespace SplitBills.Models
{
    public class Participant
    {
        public int ID { get; set; }
        public decimal ExpensesShouldBePaid { get; set; }
        public IList<decimal> Expenses { get; set; }

        public Participant(int id)
        {
            ID = id;
            Expenses = new List<decimal>();
        }

        public decimal ExpensesPaidAlready 
        {
            get
            {
                return Expenses.Sum();
            }
        }
    }
}
