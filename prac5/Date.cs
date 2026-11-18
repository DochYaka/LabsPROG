using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Практика5
{
    public class Date
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }

        public Date()
        {
            Year = 2025;
            Month = "Январь";
            Day = 1;
        }

        public Date(int year, int mounth, int day)
        {
            this.Year = (year >= 1 || year <= 2025) ? year : 1;
            this.Day = (day >= 1 || day <= 31) ? day : 1;
        }
    }
}