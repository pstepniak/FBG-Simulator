using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FBGManagement
{
    class Range
    {
        public double minValue { get; set; }
        public double maxValue { get; set; }
        public double step { get; set; }
        public Range(double minValue, double maxValue, double step)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
        }

    }
}
