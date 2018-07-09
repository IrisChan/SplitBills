using System;
using System.Collections;
using System.Collections.Generic;
using SplitBills.Models;

namespace SplitBills.Tests.Resources
{
    public class ExpensesCalculatorTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> trips = new List<object[]>();

        public ExpensesCalculatorTestData()
        {
            Trip trip1 = new Trip(3);
            Trip trip2 = new Trip(2);

            IList<Participant> participantsTrip1 = new List<Participant>();
            for (var i = 0; i < 3; i++)
            {
                participantsTrip1.Add(new Participant(i));
            }

            participantsTrip1[0].Expenses.Add(10.00m);
            participantsTrip1[0].Expenses.Add(20.00m);

            participantsTrip1[1].Expenses.Add(15.00m);
            participantsTrip1[1].Expenses.Add(15.01m);
            participantsTrip1[1].Expenses.Add(3.00m);
            participantsTrip1[1].Expenses.Add(3.01m);

            participantsTrip1[2].Expenses.Add(5.00m);
            participantsTrip1[2].Expenses.Add(9.00m);
            participantsTrip1[2].Expenses.Add(4.00m);

            trip1.Participants = participantsTrip1;

            IList<Participant> participantsTrip2 = new List<Participant>();
            for (var i = 0; i < 2; i++)
            {
                participantsTrip2.Add(new Participant(i));
            }

            participantsTrip2[0].Expenses.Add(8.00m);
            participantsTrip2[0].Expenses.Add(6.00m);

            participantsTrip2[1].Expenses.Add(9.20m);
            participantsTrip2[1].Expenses.Add(6.75m);

            trip2.Participants = participantsTrip2;

            trips.Add(new object[] {
                trip1, 0, -1.99m
            });

            trips.Add(new object[] {
                trip1, 1, -8.01m
            });

            trips.Add(new object[] {
                trip1, 2, 10.01m
            });

            trips.Add(new object[] {
                trip2, 0, 0.98m
            });

            trips.Add(new object[] {
                trip2, 1, -0.98m
            });
        }

        public IEnumerator<object[]> GetEnumerator() => trips.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
