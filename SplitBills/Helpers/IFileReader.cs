using System;
using System.Collections.Generic;
using SplitBills.Models;

namespace SplitBills.Helpers
{
    public interface IFileReader
    {
        bool IsValidFilePath(string filePath);
        IList<Trip> ReadFromFile(string filePath);
    }
}
