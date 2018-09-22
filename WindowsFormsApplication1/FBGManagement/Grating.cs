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
        public decimal chirpParam { get; }
        bool chirpReverse { get; }
        public Chirp chirpType { get; }
        public decimal chirpMinPeriod { get; }

        public Grating(decimal period, decimal length, decimal refractiveIndexModulation, decimal neff, int parts,
                        Apodisation ApodisationType, decimal apodisationParam, bool apodisationReverse,
                        Chirp ChirpType, decimal chirpParam, bool chirpReverse, decimal chirpMinPeriod)
            : this(period, length, refractiveIndexModulation, neff, parts, ApodisationType, apodisationParam, apodisationReverse, ChirpType, chirpParam, chirpReverse)
        {
            this.chirpMinPeriod = chirpMinPeriod;
        }
        public Grating(decimal period, decimal length, decimal refractiveIndexModulation, decimal neff, int parts,
                        Apodisation ApodisationType, decimal apodisationParam, bool apodisationReverse,
                        Chirp ChirpType, decimal chirpParam, bool chirpReverse)
            : this(period, length, refractiveIndexModulation, neff, parts, ApodisationType, apodisationParam, apodisationReverse)
        {
            this.chirpType = ChirpType;
            this.chirpParam = chirpParam;
            this.chirpReverse = chirpReverse;
        }

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

            this.apodisationType = Apodisation.None;
            this.apodisationReverse = false;
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

        public enum Apodisation { Gaussian, Sinc, Sin, None }
        public enum Chirp { Linear, Gaussian, Sinc, Sin, None }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionNumber">Sekcje numerujemy od 0</param>
        /// <param name="countOfSections"></param>
        /// <returns></returns>
        public decimal ApodisationProfileForSection(int sectionNumber, int countOfSections)
        {
            return this.ApodisationProfile(this.length * (sectionNumber+0.5m) / countOfSections);
        }
        private decimal ApodisationProfile(decimal z)
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
        /// <summary>
        /// Zwraca okres uwzględniając chirp i zakres okresu na podstawie parametru minimalnego okresu.
        /// </summary>
        public decimal ChirpProfileForSection(int sectionNumber, int countOfSections)
        {
            decimal profile = this.ChirpProfile(this.length * (sectionNumber + 0.5m) / countOfSections);
            profile = profile * /*period * */ (period - chirpMinPeriod) + chirpMinPeriod;
            return profile;
        }
        /// <summary>
        /// Zwraca profil z przedziału 0-1
        /// </summary>
        private decimal ChirpProfile(decimal z)
        {
            decimal profValue;
            switch (this.chirpType)
            {
                case Chirp.Linear:
                    profValue = LinearProfile(z, this.chirpParam);
                    break;
                case Chirp.Gaussian:
                    profValue = GaussianProfile(z, this.chirpParam);
                    break;
                case Chirp.Sinc:
                    profValue = SincProfile(z);
                    break;
                case Chirp.Sin:
                    profValue = SinProfile(z);
                    break;
                case Chirp.None:
                    profValue = 1;
                    break;
                default:
                    profValue = 1;
                    break;
            }
            if (this.chirpReverse)
            {
                profValue = 1 - profValue;
            };
            return profValue;
        }
        private decimal LinearProfile(decimal z, decimal directionalFactor)
        {
            return (decimal)z/length; //profil rosnący, dla malejącego trzeba zrobić reverse. Pełny wzór: v0+z*(period-v0)/length; gdzie v0 to minimalny okres.
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
        public Grating Copy()
        {
            return new Grating(this.period, this.length, this.refractiveIndexModulation, this.neff, this.parts, this.apodisationType, this.apodisationParam, this.apodisationReverse);
        }
    }
}
