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

        public Apodization apodization { get; }

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

        public enum Apodization {Gaussian, Sinc, Sin, None}

        public decimal profile(decimal z, decimal param)
        {
            switch (this.apodization)
            {
                case Apodization.Gaussian:
                    return gaussianProfile(z, param);
                case Apodization.Sinc:
                    return sincProfile(z);
                case Apodization.Sin:
                    return sinProfile(z);
                case Apodization.None:
                default:
                    return 1;
            }
        }
        public decimal gaussianProfile(decimal z, decimal sigma)
        {
            return (decimal)Math.Exp((double)(-sigma * (decimal)Math.Pow((double)((z - length / 2) / length), 2)));
        }
        public decimal sincProfile(decimal z)
        {
            return (decimal)Math.Sin((double)(2*(decimal)Math.PI*(z-length/2)/length))/(2*(decimal)Math.PI*(z-length/2)/length);
        }
        public decimal  sinProfile(decimal z)
        {
            return (decimal)Math.Sin((double)((decimal)Math.PI * z / length));
        }
        public static double gaussianApod(double x, double mu, double sigma)
        {
            return Math.Exp(-Math.Pow(x - mu,2) / (2 * Math.Pow(sigma, 2))) / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2));
        }
        public static double linearApod(double x)
        {
            return 1;
        }
    }
}
