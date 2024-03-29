﻿using System;
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
        public int countOfProbes { get; } //ilość długości fali
        public decimal s { get; } //pierwsza długość fali dla której symulujemy
        public decimal s2 { get; } //ostatnia długość fali dla której symulujemy
        private int countOfSections;
        private decimal t;

        //double j = 0.5; //to chyba jakiś współczynnik tłumienia


        public Simulation(int countOfProbe, decimal minimalWavelength, decimal maximalWavelength)
        {
            countOfProbes = countOfProbe;
            s = minimalWavelength;// + ((maximalWavelength - minimalWavelength) / countOfProbes);
            s2 = maximalWavelength;
        }
        public List<decimal> SimulateWithDividedGrating(Grating grating, out List<decimal> wavelengths)
        {
            decimal gratingPeriod = grating.period;
            decimal gratingNeff = grating.neff;
            decimal virtualGratingLength;
            decimal virtualGratingRim;
            Grating virtualGrating = null;
            List<List<decimal>> partsResults = new List<List<decimal>>();
            List<decimal> outWavelengths = new List<decimal>();
            List<decimal> results = new List<decimal>();
            for (int i = 0; i < grating.parts; i++)
            {
                virtualGratingLength = grating.length / grating.parts;
                virtualGratingRim = grating.refractiveIndexModulation * grating.ApodisationProfileForSection(i, grating.parts); //uwzględnienie apodyzacji
                virtualGrating = new Grating(grating.ChirpProfileForSection(i, grating.parts), virtualGratingLength, virtualGratingRim, gratingNeff, 1); //uwzględnienie chirpu
                partsResults.Add(this.Simulate(virtualGrating, out outWavelengths));
            }
            wavelengths = outWavelengths;
            for (int i = 0; i < this.countOfProbes; i++)
            {
                results.Add(1);
            }
            foreach (List<decimal> partResults in partsResults)
            {
                for (int i = 0; i < this.countOfProbes; i++)
                {
                    decimal partResult = results.ElementAt(i) * partResults.ElementAt(i);
                    results[i] = partResult;
                }
            }
            //virtualGrating.apodisationType = Grating.Apodisation.None;
            return results;

        }
        public List<decimal> Simulate(Grating grating, out List<decimal> wavelengths)
        {
            //okresy siatki
            countOfSections = grating.parts;
            t = 1 / (decimal)countOfSections;

            for (int i = 0; i < countOfSections; i++)
            {
                decimal period_i = grating.ChirpProfileForSection(i, countOfSections);
                period_i = period_i / (decimal)Math.Pow(10, -9);
                x.Add(period_i);
                //x.Add(grating.period / (decimal)Math.Pow(10, -9)); //tu należy uwzględnić chirp
            }
            Utils.SaveArrayAsCSV(x.ToArray(), "C:\\FBG\\_x.csv");
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
            Utils.SaveArrayAsCSV(lj.ToArray(), "C:\\FBG\\_lj.csv");
            List<decimal> okresy = new List<decimal>(); //długość - ilość sekcji
            List<decimal> lambdaBy = new List<decimal>(); //długość - ilość sekcji
            List<decimal> apodisation = new List<decimal>(); //długość - ilość sekcji
            /*for nn = 1:1:ilosc_sekcji;*/
            for (int i = 0; i < countOfSections; i++)
            {
                decimal okres_i = (decimal)Math.Pow(10, -9) * x[i];
                okresy.Add(okres_i);
                decimal lambdaBy_i = 2m * grating.neff * okresy[i];
                lambdaBy.Add(lambdaBy_i);

                //apodyzacja dla sekcji w oparciu o dane zapisane na siatce
                decimal apodyzacja_i = grating.ApodisationProfileForSection(i, countOfSections);
                apodisation.Add(apodyzacja_i);
            }
            Utils.SaveArrayAsCSV(okresy.ToArray(), "C:\\FBG\\_okresy.csv");
            Utils.SaveArrayAsCSV(lambdaBy.ToArray(), "C:\\FBG\\_lambdaBy.csv");
            wavelengths = new List<decimal>();
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
                wavelengths.Add((s + (i + 1) * ((s2 - s) / countOfProbes)) * (decimal)Math.Pow(10, -9));
            }
            Utils.SaveArrayAsCSV(wavelengths.ToArray(), "C:\\FBG\\_wavelengths.csv");
            decimal[,] k = new decimal[countOfProbes, countOfSections];
            decimal[,] delta = new decimal[countOfProbes, countOfSections];
            decimal[,] k2 = new decimal[countOfProbes, countOfSections];
            decimal[,] sgm = new decimal[countOfProbes, countOfSections];
            decimal[,] sgm2 = new decimal[countOfProbes, countOfSections];
            DecComplex[,] gammaB = new DecComplex[countOfProbes, countOfSections];

            for (int ll = 0; ll < countOfProbes; ll++)
            {
                for (int nn = 0; nn < countOfSections; nn++) //w pętli lecimy po lambda oraz dla danego z
                {
                    k[ll, nn] = ((decimal)Math.PI / wavelengths.ElementAt(ll)) * grating.refractiveIndexModulation * apodisation.ElementAt(nn); //wzór 3 -7
                    delta[ll, nn] = 2 * (decimal)Math.PI * grating.neff * ((1 / wavelengths.ElementAt(ll)) - (1 / lambdaBy.ElementAt(nn)));//!zaokr
                    sgm[ll, nn] = delta[ll, nn]; //chirp
                    k2[ll, nn] = (decimal)Math.Pow((double)k[ll, nn], 2);
                    sgm2[ll, nn] = (decimal)Math.Pow((double)sgm[ll, nn], 2);
                    if (k2[ll, nn] > sgm2[ll, nn])
                    {
                        gammaB[ll, nn] = new DecComplex(Math.Pow((double)(k2[ll, nn] - sgm2[ll, nn]), 0.5),0); //wzór 3-14
                    }
                    else if (k2[ll, nn] < sgm2[ll, nn])
                    {
                        gammaB[ll, nn] = DecComplex.ImaginaryOne * new DecComplex(Math.Pow((double)(sgm2[ll, nn] - k2[ll, nn]), 0.5),0); //wzór 3-15
                    }
                    else
                    {
                        gammaB[ll, nn] = new DecComplex(0.0000000000001m,0.0000000000001m); //zabezpieczenie przed błędem dzielenia przez 0 - jak się okazuje, ta wartość wykorzystywana jest później w dzieleniu
                    }
                }

            }
            //Utils.SaveArrayAsCSV(k.ToArray(), "C:\\FBG\\_k.csv");
            //Utils.SaveArrayAsCSV(delta.ToArray(), "C:\\FBG\\_delta.csv");
            //Utils.SaveArrayAsCSV(k2.ToArray(), "C:\\FBG\\_k2.csv");
            //Utils.SaveArrayAsCSV(sgm.ToArray(), "C:\\FBG\\_sgm.csv");
            //Utils.SaveArrayAsCSV(sgm2.ToArray(), "C:\\FBG\\_sgm2.csv");

            // macierz Fj, gdzie j mówi o pozycji rozpatrywania siatki
            // (j jest od L do 0 !!!!!, a nie odwrotnie !!!!!)
            // wszystkie Fj trzeba zapisywać, żeby potem je wymnożyć

            DecComplex[,] F11 = new DecComplex[countOfProbes, countOfSections];
            DecComplex[,] F12 = new DecComplex[countOfProbes, countOfSections];
            DecComplex[,] F21 = new DecComplex[countOfProbes, countOfSections];
            DecComplex[,] F22 = new DecComplex[countOfProbes, countOfSections];

            for (int ll = 0; ll < countOfProbes; ll++)
            {
                for (int nn = 0; nn < countOfSections; nn++) //w pętli lecimy po lambda oraz dla danego z
                {
                    F11[ll,nn] = DecComplex.Cosh(gammaB[ll, nn] * (double)lj.ElementAt(nn)) + DecComplex.ImaginaryOne*((double)(sgm[ll, nn]) / gammaB[ll, nn]) *
                        DecComplex.Sinh(gammaB[ll, nn] * (double)lj.ElementAt(nn));
                    F12[ll,nn] = DecComplex.ImaginaryOne*((double)k[ll, nn] / gammaB[ll, nn]) * DecComplex.Sinh(gammaB[ll, nn] * (double)lj.ElementAt(nn));
                    F21[ll, nn] = DecComplex.Conj(F12[ll, nn]);
                    F22[ll, nn] = DecComplex.Conj(F11[ll, nn]);
                }
            }


            DecComplex[,,,] D = new DecComplex[2, 2, countOfProbes, countOfSections];
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
            //KOD W MATLABIE: T(:,:,f)=D(:,:,f,(ilosc_sekcji));

            DecComplex[,,] T = new DecComplex[2, 2, countOfProbes];
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
                    DecComplex T00 = T[0, 0, fi] * D[0, 0, fi, e] + T[0, 1, fi] * D[1, 0, fi, e];
                    DecComplex T01 = T[0, 0, fi] * D[0, 1, fi, e] + T[0, 1, fi] * D[1, 1, fi, e];
                    DecComplex T10 = T[1, 0, fi] * D[0, 0, fi, e] + T[1, 1, fi] * D[1, 0, fi, e];
                    DecComplex T11 = T[1, 0, fi] * D[0, 1, fi, e] + T[1, 1, fi] * D[1, 1, fi, e];

                    T[0, 0, fi] = T00;
                    T[0, 1, fi] = T01;
                    T[1, 0, fi] = T10;
                    T[1, 1, fi] = T11;
                }
                DecComplex value = 1d / T[0, 0, fi];
                decimal doubleValue = (decimal)DecComplex.Abs(value);
                Ry.Add(doubleValue);

                fi = fi + 1;
            }

            //normalizacja Ry:
            List<decimal> NormRy = new List<decimal>();
            decimal norm = Ry.Max();
            //foreach (decimal item in Ry)
            //{
            //    NormRy.Add(item / norm);
            //}

            //return NormRy;
            return Ry;
        }

        public void Clear()
        {
            x.Clear();
        }

    }
}
