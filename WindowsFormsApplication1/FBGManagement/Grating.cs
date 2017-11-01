using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FBGManagement
{
    class Grating
    {
        private double length;
        private double period;
        private double refractiveIndexModulation; //delta_n
        private const double neff = 1.44688;
        private const double lambdaB = 0.0000015312;
        //okres=lambdaB/(2*pi*neff);% delta n

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
            refractiveIndexModulation = 0.0004;
        }
        public Grating()
        {
            length = 0.1; //metra
            period = lambdaB / (2 * Math.PI * neff);
            refractiveIndexModulation = 0.0004;
        }
    }
}
