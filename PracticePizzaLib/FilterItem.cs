using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeRestLib
{
    public class FilterItem
    {
        public double LowCost { get; set; }
        public double HighCost { get; set; }

        public FilterItem() { }

        public FilterItem(double lowCost, double highCost)
        {
            LowCost = lowCost;
            HighCost = highCost;
        }
    }
}
