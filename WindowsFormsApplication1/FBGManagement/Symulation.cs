using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace WindowsFormsApplication1.FBGManagement
{
    class Simulation
    {
        List<decimal> x = new List<decimal>();

        /*Dane symulacji*/
        int countOfProbes; //ilość długości fali
        decimal s; //pierwsza długość fali dla której symulujemy
        decimal s2; //ostatnia długość fali dla której symulujemy
        int countOfSections;
        decimal t;

        //double j = 0.5; //to chyba jakiś współczynnik tłumienia


        public Simulation(int countOfProbe, decimal minimalWavelength, decimal maximalWavelength)
        {
            countOfProbes = countOfProbe;
            s = minimalWavelength + ((maximalWavelength - minimalWavelength) / countOfProbes);
            s2 = maximalWavelength;
        }
        public List<decimal> Simulate(Grating grating)
        {
            //okresy siatki
            countOfSections = grating.parts;
            t = 1 / (decimal)countOfSections;

            for (int i = 0; i < countOfSections; i++)
            {
                x.Add(grating.period / (decimal)Math.Pow(10, -9));
            }

            /*Dane siatki*/
            //double neff = 1.44688; //efektywny współczynnik załamania
            //double L = 10000 * Math.Pow(10, -6); //długość siatki
            //double lambdaB = 1531.2 * Math.Pow(10, -9); //długość fali Bragga
            //double okres = lambdaB / (2 * Math.PI * neff);
            //double delta_n = 0.00010; //delta n
            //double neff = 1.44688; //efektywny współczynnik załamania
            //double L = 10000 * Math.Pow(10, -6); //długość siatki
            //double lambdaB = 1531.2 * Math.Pow(10, -9); //długość fali Bragga
            //double okres = lambdaB / (2 * Math.PI * neff);
            //double delta_n = 0.00010; //delta n

            /*lj = (t:t: 1)*L;*/
            List<decimal> lj = new List<decimal>();
            for (decimal i = t; i <= 1 || lj.Count < countOfSections; i = i + t)
            {
                if (i > 1) i = 1;//ZABEZPIECZENIE PRZED ZŁYM ZAOKRĄGLENIEM
                lj.Add(i* grating.length);
            }

            List<decimal> okresy = new List<decimal>(); //długość - ilość sekcji
            List<decimal> lambdaBy = new List<decimal>(); //długość - ilość sekcji
            List<decimal> apodisation = new List<decimal>(); //długość - ilość sekcji
            /*for nn = 1:1:ilosc_sekcji;*/
            for (int i = 0; i < countOfSections; i++)
            {
                decimal okres_i = (decimal)Math.Pow(10, -9) * x[i];
                okresy.Add(okres_i);
                decimal lambdaBy_i = 2m * (decimal)Math.PI * grating.neff * okresy[i];
                lambdaBy.Add(lambdaBy_i);

                //współczynnik funkcji apodyzacji
                decimal a = 80;
                decimal apodyzacja_i = 1;//Math.Exp(-a * Math.Pow(((lj.ElementAt(i) - L / 2) / L), 2));
                //apodyzacja(nn) = exp(-a * ((lj(nn) - L / 2) / L) ^ 2);
                apodisation.Add(apodyzacja_i);
            }

            List<decimal> wavelenghts = new List<decimal>();
            decimal incrementStep = ((s2 - s) / (decimal)countOfProbes);
            //double stepRounding = Math.Log10((double)countOfProbes);
            //stepRounding = Math.Ceiling(stepRounding);
            //incrementStep = Math.Round(incrementStep, (Int32)stepRounding);
            //incrementStep = Math.Round(incrementStep, 8); //8 wstawiamy na sztywno - powinno wystarczyć
            //for (decimal i = s;i<=s2;i=i+ incrementStep)
            //{
            //    wavelenghts.Add(i * (decimal)Math.Pow(10, -9));
            //}
            for (int i = 0; i < countOfProbes; i++)
            {
                wavelenghts.Add((s + (i + 1) * ((s2 - s) / countOfProbes)) * (decimal)Math.Pow(10, -9));
            }

                decimal[,] k = new decimal[countOfProbes, countOfSections];
            decimal[,] delta = new decimal[countOfProbes, countOfSections];
            decimal[,] k2 = new decimal[countOfProbes, countOfSections];
            decimal[,] sgm = new decimal[countOfProbes, countOfSections];
            decimal[,] sgm2 = new decimal[countOfProbes, countOfSections];
            Complex[,] gammaB = new Complex[countOfProbes, countOfSections];

            for (int ll = 0; ll < countOfProbes; ll++)
            {
                for (int nn = 0; nn < countOfSections; nn++) //w pętli lecimy po lambda oraz dla danego z
                {
                    k[ll, nn] = ((decimal)Math.PI / wavelenghts.ElementAt(ll)) * grating.refractiveIndexModulation * apodisation.ElementAt(nn);
                    delta[ll, nn] = 2 * (decimal)Math.PI * grating.neff * ((1 / wavelenghts.ElementAt(ll)) - (1 / lambdaBy.ElementAt(nn)));
                    sgm[ll, nn] = delta[ll, nn]; //chirp
                    k2[ll, nn] = (decimal)Math.Pow((double)k[ll, nn], 2);
                    sgm2[ll, nn] = (decimal)Math.Pow((double)sgm[ll, nn], 2);
                    if (k2[ll, nn] > sgm2[ll, nn])
                    {
                        gammaB[ll, nn] = Math.Pow((double)(k2[ll, nn] - sgm2[ll, nn]), 0.5); //wzór 3-14
                    }
                    else if (k2[ll, nn] < sgm2[ll, nn])
                    {
                        gammaB[ll, nn] = Complex.ImaginaryOne * Math.Pow((double)(sgm2[ll, nn] - k2[ll, nn]), 0.5); //wzór 3-15
                    }
                    else
                    {
                        gammaB[ll, nn] = 0;
                    }
                }
            }


            // macierz Fj, gdzie j mówi o pozycji rozpatrywania siatki
            // (j jest od L do 0 !!!!!, a nie odwrotnie !!!!!)
            // wszystkie Fj trzeba zapisywać, żeby potem je wymnożyć

            Complex[,] F11 = new Complex[countOfProbes, countOfSections];
            Complex[,] F12 = new Complex[countOfProbes, countOfSections];
            Complex[,] F21 = new Complex[countOfProbes, countOfSections];
            Complex[,] F22 = new Complex[countOfProbes, countOfSections];

            for (int ll = 0; ll < countOfProbes; ll++)
            {
                for (int nn = 0; nn < countOfSections; nn++) //w pętli lecimy po lambda oraz dla danego z
                {
                    F11[ll,nn] = Complex.Cosh(gammaB[ll, nn] * (double)lj.ElementAt(nn)) + Complex.ImaginaryOne*((double)(sgm[ll, nn]) / gammaB[ll, nn]) * 
                        Complex.Sinh(gammaB[ll, nn] * (double)lj.ElementAt(nn));
                    F12[ll,nn] = Complex.ImaginaryOne*((double)k[ll, nn] / gammaB[ll, nn]) * Complex.Sinh(gammaB[ll, nn] * (double)lj.ElementAt(nn));
                    F21[ll, nn] = F12[ll, nn];
                    F22[ll, nn] = F11[ll, nn];
                }
            }


            Complex[,,,] D = new Complex[2, 2, countOfProbes, countOfSections];
            for (int c = 0; c < countOfProbes; c++)
            {
                for (int d = 0; d < countOfSections; d++)
                {
                    D[0, 0, c, d] = F11[c, d];
                    D[0, 1, c, d] = F12[c, d];
                    D[1, 0, c, d] = F21[c, d];
                    D[1, 1, c, d] = F22[c, d];
                }
            }
            //UWAGA, TA PĘTLA NIE JEST PEWNA, MOŻE CHODZIŁO O COŚ INNEGO, ORYGINALNY KOD: T(:,:,f)=D(:,:,f,(ilosc_sekcji));

            Complex [,,] T = new Complex[2, 2, countOfProbes];
            for (int f = 0; f < countOfProbes; f++)
            {
                T[0, 0, f] = D[0, 0, f, countOfSections-1];
                T[0, 1, f] = D[0, 1, f, countOfSections-1];
                T[1, 0, f] = D[1, 0, f, countOfSections-1];
                T[1, 1, f] = D[1, 1, f, countOfSections-1];
            }

            List<decimal> Ry = new List<decimal>();

            int fi = 0; //counter
            while (fi <countOfProbes)
            {
                for (int e = countOfSections-1;e>=0;--e)
                {
                    T[0, 0, fi] = T[0, 0, fi] * D[0, 0, fi, e];
                    T[0, 1, fi] = T[0, 1, fi] * D[0, 1, fi, e];
                    T[1, 0, fi] = T[1, 0, fi] * D[1, 0, fi, e];
                    T[1, 1, fi] = T[1, 1, fi] * D[1, 1, fi, e];
                }
                Complex value = 1 / T[0, 0, fi];
                decimal doubleValue = (decimal)Complex.Abs(value);
                Ry.Add(doubleValue);

                fi = fi + 1;
            }

            //normalizacja Ry:
            List<decimal> NormRy = new List<decimal>();
            decimal norm = Ry.Max();
            foreach (decimal item in Ry)
            {
                NormRy.Add(item / norm);
            }

            //return NormRy;
            return Ry;
        }

    }
}
