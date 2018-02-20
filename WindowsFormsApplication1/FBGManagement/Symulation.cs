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
        List<double> x = new List<double>();

        /*Dane symulacji*/
        int countOfProbes; //ilość długości fali
        double s; //pierwsza długość fali dla której symulujemy
        double s2; //ostatnia długość fali dla której symulujemy
        int countOfSections;
        double t;

        //double j = 0.5; //to chyba jakiś współczynnik tłumienia


        public Simulation(int countOfProbe, double minimalWavelength, double maximalWavelength)
        {
            countOfProbes = countOfProbe;
            s = minimalWavelength + ((maximalWavelength - minimalWavelength) / countOfProbes);
            s2 = maximalWavelength;
        }
        public List<double> Simulate(Grating grating)
        {
            //okresy siatki
            countOfSections = grating.parts;
            t = 1 / (double)countOfSections;

            for (int i = 0; i < countOfSections; i++)
            {
                x.Add(grating.period / Math.Pow(10, -9));
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
            List<double> lj = new List<double>();
            for (double i = t; i <= 1 || lj.Count < countOfSections; i = i + t)
            {
                if (i > 1) i = 1;
                lj.Add(i* grating.length);
            }

            List<double> okresy = new List<double>(); //długość - ilość sekcji
            List<double> lambdaBy = new List<double>(); //długość - ilość sekcji
            List<double> apodisation = new List<double>(); //długość - ilość sekcji
            /*for nn = 1:1:ilosc_sekcji;*/
            for (int i = 0; i < countOfSections; i++)
            {
                double okres_i = Math.Pow(10, -9) * x[i];
                okresy.Add(okres_i);
                double lambdaBy_i = 2 * Math.PI * grating.neff * okresy[i];
                lambdaBy.Add(lambdaBy_i);

                //współczynnik funkcji apodyzacji
                double a = 80;
                double apodyzacja_i = 1;//Math.Exp(-a * Math.Pow(((lj.ElementAt(i) - L / 2) / L), 2));
                //apodyzacja(nn) = exp(-a * ((lj(nn) - L / 2) / L) ^ 2);
                apodisation.Add(apodyzacja_i);
            }

            List<double> wavelenghts = new List<double>();
            for (double i = s;i<=s2;i=i+((s2-s)/(double)countOfProbes))
            {
                wavelenghts.Add(i * Math.Pow(10, -9));
            }

            double[,] k = new double[countOfProbes, countOfSections];
            double[,] delta = new double[countOfProbes, countOfSections];
            double[,] k2 = new double[countOfProbes, countOfSections];
            double[,] sgm = new double[countOfProbes, countOfSections];
            double[,] sgm2 = new double[countOfProbes, countOfSections];
            Complex[,] gammaB = new Complex[countOfProbes, countOfSections];

            for (int ll = 0; ll < countOfProbes; ll++)
            {
                for (int nn = 0; nn < countOfSections; nn++) //w pętli lecimy po lambda oraz dla danego z
                {
                    k[ll, nn] = (Math.PI / wavelenghts.ElementAt(ll)) * grating.refractiveIndexModulation * apodisation.ElementAt(nn);
                    delta[ll, nn] = 2 * Math.PI * grating.neff * ((1 / wavelenghts.ElementAt(ll)) - (1 / lambdaBy.ElementAt(nn)));
                    sgm[ll, nn] = delta[ll, nn]; //chirp
                    k2[ll, nn] = Math.Pow(k[ll, nn], 2);
                    sgm2[ll, nn] = Math.Pow(sgm[ll, nn], 2);
                    if (k2[ll, nn] > sgm2[ll, nn])
                    {
                        gammaB[ll, nn] = Math.Pow(k2[ll, nn] - sgm2[ll, nn], 0.5); //wzór 3-14
                    }
                    else if (k2[ll, nn] < sgm2[ll, nn])
                    {
                        gammaB[ll, nn] = Complex.ImaginaryOne * Math.Pow(sgm2[ll, nn] - k2[ll, nn], 0.5); //wzór 3-15
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
                    F11[ll,nn] = Complex.Cosh(gammaB[ll, nn] * lj.ElementAt(nn)) + Complex.ImaginaryOne*(sgm[ll, nn] / gammaB[ll, nn]) * 
                        Complex.Sinh(gammaB[ll, nn] * lj.ElementAt(nn));
                    F12[ll,nn] = Complex.ImaginaryOne*(k[ll, nn] / gammaB[ll, nn]) * Complex.Sinh(gammaB[ll, nn] * lj.ElementAt(nn));
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

            List<double> Ry = new List<double>();

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
                double doubleValue = Complex.Abs(value);
                Ry.Add(doubleValue);

                fi = fi + 1;
            }

            //normalizacja Ry:
            List<double> NormRy = new List<double>();
            double norm = Ry.Max();
            foreach (double item in Ry)
            {
                NormRy.Add(item / norm);
            }

            //return NormRy;
            return Ry;
        }

    }
}
