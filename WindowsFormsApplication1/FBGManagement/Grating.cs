using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FBGManagement
{
    class Grating
    {
        public decimal length { get; }
        public decimal period { get; }
        public decimal refractiveIndexModulation { get; }
        public decimal neff { get; }
        public decimal lambdaB {get;}
        //period=lambdaB/(2*pi*neff);
        public int parts { get; }

        public Grating(decimal period, decimal length, decimal refractiveIndexModulation, decimal neff, int parts)
        {
            this.neff = neff;
            this.length = length;
            this.period = period;
            this.refractiveIndexModulation = refractiveIndexModulation;
            this.parts = parts;
            lambdaB = period / (2 * (decimal)Math.PI * neff);
        }
        public Grating(decimal period, decimal length, decimal refractiveIndexModulation, decimal neff) :  this(period, length,refractiveIndexModulation,neff, 10)
        {

        }
        public Grating(decimal period, decimal length, decimal refractiveIndexModulation) : this(period, length, refractiveIndexModulation, 1.44688m)
        {

        }
        public Grating(decimal period, decimal length) : this(period, length, 0.0001m)
        {
        }
        public Grating() : this(0.0000015312m / (2m * (decimal)Math.PI * 1.44688m), 0.1m)
        {
            this.neff = 1.44688m;
            length = 0.1m; //metra
            lambdaB = 0.0000015312m;
            period = 0.0000015312m / (2m * (decimal)Math.PI * neff);
            refractiveIndexModulation = 0.0004m;
        }
    }
}
