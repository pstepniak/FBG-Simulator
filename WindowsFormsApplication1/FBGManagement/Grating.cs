using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FBGManagement
{
    class Grating
    {
        private int length;
        private double period;
        private double refractiveIndexModulation;

        public Grating(double period, int length, double refractiveIndexModulation)
        {
            this.length = length;
            this.period = period;
            this.refractiveIndexModulation = refractiveIndexModulation;
        }
        public Grating(double period, int length)
        {
            this.length = length;
            this.period = period;
            refractiveIndexModulation = 0.0001;
        }
    }
}
