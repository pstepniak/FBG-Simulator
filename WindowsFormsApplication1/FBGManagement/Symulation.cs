using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace WindowsFormsApplication1.FBGManagement
{
    class Symulation
    {
        double[] x = { 168.43, 168.43, 168.43, 168.43, 168.43, 168.43, 168.43, 168.43 };

        /*Dane symulacji*/
        int ilosc_lambda; //ilość długości fali
        double s; //na razie nie wiem co to jest ;p
        double s2;
        int ilosc_sekcji;
        double t;

        //double j = 0.5; //to chyba jakiś współczynnik tłumienia


        public Symulation(int countOfProbe, double minimalPeriod, double maximalPeriod)
        {
            //ilosc_lambda = 1000;
            ilosc_lambda = countOfProbe;
            //s = 1530.75 + (3 / ilosc_lambda);
            s = minimalPeriod + ((maximalPeriod - minimalPeriod) / ilosc_lambda);
            //s2 = 1533.75;
            s2 = maximalPeriod;
            ilosc_sekcji = x.Length;
            t = 1 / (double)ilosc_sekcji;
        }
        public List<double> Symulate()
        {
            

            /*Dane siatki*/
            double neff = 1.44688; //efektywny współczynnik załamania
            double L = 10000 * Math.Pow(10,-6); //długość siatki
            double lambdaB = 1531.2 * Math.Pow(10, -9); //długość fali Bragga
            double okres = lambdaB / (2 * Math.PI * neff);
            double delta_n = 0.00040; //delta n

            /*lj = (t:t: 1)*L;*/
            List<double> lj = new List<double>();
            for (double i = t; i <= 1; i = i + t)
            {
                lj.Add(i * L);
            }

            List<double> okresy = new List<double>(); //długość - ilość sekcji
            List<double> lambdaBy = new List<double>(); //długość - ilość sekcji
            List<double> apodyzacja = new List<double>(); //długość - ilość sekcji
            /*for nn = 1:1:ilosc_sekcji;*/
            for (int i = 0; i < ilosc_sekcji; i++)
            {
                double okres_i = Math.Pow(10, -9) * x[i];
                okresy.Add(okres_i);
                double lambdaBy_i = 2 * Math.PI * neff * okresy[i];
                lambdaBy.Add(lambdaBy_i);

                //współczynnik funkcji apodyzacji
                double a = 80;
                double apodyzacja_i = 1;//Math.Exp(-a * Math.Pow(((lj.ElementAt(i) - L / 2) / L), 2));
                //apodyzacja(nn) = exp(-a * ((lj(nn) - L / 2) / L) ^ 2);
                apodyzacja.Add(apodyzacja_i);
            }

            List<double> lambda = new List<double>();
            for (double i = s;i<=s2;i=i+((s2-s)/(double)ilosc_lambda)) //chyba drut, bo to 3 to jest różnica między s2 a s1
            {
                lambda.Add(i * Math.Pow(10, -9));
            }

            double[,] k = new double[ilosc_lambda, ilosc_sekcji];
            double[,] delta = new double[ilosc_lambda, ilosc_sekcji];
            double[,] k2 = new double[ilosc_lambda, ilosc_sekcji];
            double[,] sigma = new double[ilosc_lambda, ilosc_sekcji];
            double[,] sigma2 = new double[ilosc_lambda, ilosc_sekcji];
            Complex[,] gammaB = new Complex[ilosc_lambda, ilosc_sekcji];

            //Complex temp = new Complex(1, 2);
            
            for (int ll = 0; ll < ilosc_lambda; ll++)
            {
                for (int nn = 0; nn < ilosc_sekcji; nn++) //w pętli lecimy po lambda oraz dla danego z
                {
                    k[ll, nn] = (Math.PI / lambda.ElementAt(ll)) * delta_n * apodyzacja.ElementAt(nn);
                    delta[ll, nn] = 2 * Math.PI * neff * ((1 / lambda.ElementAt(ll)) - (1 / lambdaBy.ElementAt(nn)));
                    sigma[ll, nn] = delta[ll, nn]; //chirp
                    k2[ll, nn] = Math.Pow(k[ll, nn], 2);
                    sigma2[ll, nn] = Math.Pow(sigma[ll, nn], 2);
                    if (k2[ll, nn] > sigma2[ll, nn])
                    {

                        gammaB[ll, nn] = Math.Pow(k2[ll, nn] - sigma2[ll, nn], 0.5); //wzór 3-14
                    }
                    else if (k2[ll, nn] < sigma2[ll, nn])
                    {
                        gammaB[ll, nn] = Complex.ImaginaryOne*Math.Pow(sigma2[ll, nn] - k2[ll, nn], 0.5); //wzór 3-15
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

            Complex[,] F11 = new Complex[ilosc_lambda, ilosc_sekcji];
            Complex[,] F12 = new Complex[ilosc_lambda, ilosc_sekcji];
            Complex[,] F21 = new Complex[ilosc_lambda, ilosc_sekcji];
            Complex[,] F22 = new Complex[ilosc_lambda, ilosc_sekcji];

            for (int ll = 0; ll < ilosc_lambda; ll++)
            {
                for (int nn = 0; nn < ilosc_sekcji; nn++) //w pętli lecimy po lambda oraz dla danego z
                {
                    F11[ll,nn] = Complex.Cosh(gammaB[ll, nn] * lj.ElementAt(nn)) + Complex.ImaginaryOne*(sigma[ll, nn] / gammaB[ll, nn]) * Complex.Sinh(gammaB[ll, nn] * lj.ElementAt(nn));
                    F12[ll,nn] = Complex.ImaginaryOne*(k[ll, nn] / gammaB[ll, nn]) * Complex.Sinh(gammaB[ll, nn] * lj.ElementAt(nn));
                    F21[ll, nn] = F12[ll, nn];
                    F22[ll, nn] = F11[ll, nn];
                }
            }


            Complex[,,,] D = new Complex[2, 2, ilosc_lambda, ilosc_sekcji];
            for (int c = 0; c < ilosc_lambda; c++)
            {
                for (int d = 0; d < ilosc_sekcji; d++)
                {
                    D[0, 0, c, d] = F11[c, d];
                    D[0, 1, c, d] = F12[c, d];
                    D[1, 0, c, d] = F21[c, d];
                    D[1, 1, c, d] = F22[c, d];
                }
            }
            Complex [,,] T = new Complex[2, 2, ilosc_lambda];

            //UWAGA, TA PĘTLA NIE JEST PEWNA, MOŻE CHODZIŁO O COŚ INNEGO, ORYGINALNY KOD: T(:,:,f)=D(:,:,f,(ilosc_sekcji));
            for (int f = 0; f < ilosc_lambda; f++)
            {
                T[0, 0, f] = D[0, 0, f, ilosc_sekcji-1];
                T[0, 1, f] = D[0, 1, f, ilosc_sekcji-1];
                T[1, 0, f] = D[1, 0, f, ilosc_sekcji-1];
                T[1, 1, f] = D[1, 1, f, ilosc_sekcji-1];
            }

            List<double> Ry = new List<double>();

            int fi = 0; //licznik
            while (fi <ilosc_lambda)
            {
                for (int e = ilosc_sekcji-1;e>=0;--e)
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
