using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FBGManagement
{
    class Grating
    {
        public double length { get; }
        public double period { get; }
        public double refractiveIndexModulation { get; } //delta_n
        public double neff { get; }
        //public const double lambdaB = 0.0000015312;
        public double lambdaB {get;}
        //okres=lambdaB/(2*pi*neff);% delta n

        public Grating(double period, double length, double refractiveIndexModulation, double neff)
        {
            this.neff = neff;
            this.length = length;
            this.period = period;
            this.refractiveIndexModulation = refractiveIndexModulation;
            lambdaB = period / (2 * Math.PI * neff);
        }
        public Grating(double period, double length, double refractiveIndexModulation) : this(period, length, refractiveIndexModulation, 1.44688)
        {

        }
        public Grating(double period, double length) : this(period, length, 0.0001)
        {
        }
        public Grating() : this(0.0000015312 / (2 * Math.PI * 1.44688), 0.1)
        {
            this.neff = 1.44688;
            length = 0.1; //metra
            lambdaB = 0.0000015312;
            period = 0.0000015312 / (2 * Math.PI * neff);
            refractiveIndexModulation = 0.0004;
        }
    }
}
