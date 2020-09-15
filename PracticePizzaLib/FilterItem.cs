using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeRestLib
{
    public class FilterItem
    {
        public double HighCost { get; set; }
        public double LowCost { get; set; }

        public FilterItem() { }

        public FilterItem(double highCost, double lowCost)
        {
            HighCost = highCost;
            LowCost = lowCost;
        }
    }
}
