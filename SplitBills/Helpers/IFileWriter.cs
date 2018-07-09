using System;
using System.Collections.Generic;
using SplitBills.Models;

namespace SplitBills.Helpers
{
    public interface IFileWriter
    {
        void WriteToFile(string filePath, IList<Trip> trips);
    }
}
