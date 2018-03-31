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

        public decimal apodisationParam { get; }
        bool apodisationReverse { get; }
        public Apodisation apodisationType { get; }

        public Grating(decimal period, decimal length, decimal refractiveIndexModulation, decimal neff, int parts, Apodisation ApodisationType, decimal apodisationParam, bool apodisationReverse)
            : this(period, length, refractiveIndexModulation, neff, parts)
        {
            this.apodisationType = ApodisationType;
            this.apodisationParam = apodisationParam;
            this.apodisationReverse = apodisationReverse;
        }

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

        public enum Apodisation {Gaussian, Sinc, Sin, None}

        public decimal ProfileForSection(int sectionNumber, int countOfSections)
        {
            return this.Profile(this.length * sectionNumber / countOfSections);
        }
        private decimal Profile(decimal z)
        {
            decimal profValue;
            switch (this.apodisationType)
            {
                case Apodisation.Gaussian:
                    profValue = GaussianProfile(z, this.apodisationParam);
                    break;
                case Apodisation.Sinc:
                    profValue = SincProfile(z);
                    break;
                case Apodisation.Sin:
                    profValue = SinProfile(z);
                    break;
                case Apodisation.None:
                    profValue = 1;
                    break;
                default:
                    profValue = 1;
                    break;
            }
            if (this.apodisationReverse)
            {
                profValue = 1 - profValue;
            };
            return profValue;
        }
        private decimal GaussianProfile(decimal z, decimal sigma)
        {
            return (decimal)Math.Exp((double)(-sigma * (decimal)Math.Pow((double)((z - length / 2) / length), 2)));
        }
        private decimal SincProfile(decimal z)
        {
            if ((2 * (decimal)Math.PI * (z - length / 2) / length) != 0)
            {
                return (decimal)Math.Sin((double)(2 * (decimal)Math.PI * (z - length / 2) / length)) / (2 * (decimal)Math.PI * (z - length / 2) / length);
            } else
            {
                return 1;
            }
        }
        private decimal  SinProfile(decimal z)
        {
            return (decimal)Math.Sin((double)((decimal)Math.PI * z / length));
        }
        private static double gaussianApod(double x, double mu, double sigma)
        {
            return Math.Exp(-Math.Pow(x - mu,2) / (2 * Math.Pow(sigma, 2))) / Math.Sqrt(2 * Math.PI * Math.Pow(sigma, 2));
        }
        private static double linearApod(double x)
        {
            return 1;
        }
    }
}
