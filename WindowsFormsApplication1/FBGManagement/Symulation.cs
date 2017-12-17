using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FBGManagement
{
    class Symulation
    {
        public Symulation()
        {

        }
        public void Symulate()
        {
            double[] x = { 168.43, 168.43, 168.43, 168.43, 168.43, 168.43, 168.43, 168.43 };

            /*Dane siatki*/
            double neff = 1.44688; //efektywny współczynnik załamania
            double L = 10000 * (10 ^ (-6)); //długość siatki
            double lambdaB = 1531.2 * (10 ^ (-9));
            double okres = lambdaB / (2 * Math.PI * neff);
            double delta_n = 0.00040; //delta n

            /*Dane symulacji*/
            int ilosc_lambda = 1000; //ilość długości fali
            double s = 1530.75 + (3 / ilosc_lambda); //na razie nie wiem co to jest ;p
            double s2 = 1533.75;
            int ilosc_sekcji = x.Length;
            double t = 1 / ilosc_sekcji;

            double j = 0.5; //to chyba jakiś współczynnik tłumienia

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
            for (int i = 1; i <= ilosc_sekcji; i++)
            {
                double okres_i = Math.Pow(10, -9) * x[i];
                okresy.Add(okres_i);
                double lambdaBy_i = 2 * Math.PI * neff * x[i];
                lambdaBy.Add(lambdaBy_i);

                //współczynnik funkcji apodyzacji
                double a = 80;
                double apodyzacja_i = Math.Exp(-a * Math.Pow(((lj.ElementAt(i) - L / 2) / L), 2));
                apodyzacja.Add(apodyzacja_i);
            }

            List<double> lambda = new List<double>();
            for (double i = s;i<=s2;i=i+(3/ilosc_lambda))
            {
                lambda.Add(i * Math.Pow(10, -9));
            }

            double[,] k = new double[ilosc_lambda, ilosc_sekcji];
            double[,] delta = new double[ilosc_lambda, ilosc_sekcji];
            double[,] k2 = new double[ilosc_lambda, ilosc_sekcji];
            double[,] sigma = new double[ilosc_lambda, ilosc_sekcji];
            double[,] sigma2 = new double[ilosc_lambda, ilosc_sekcji];
            double[,] gammaB = new double[ilosc_lambda, ilosc_sekcji];
            for (int ll = 0; ll < ilosc_lambda; ll++)
            {
                for (int nn = 0; nn < ilosc_lambda; nn++) //w pętli lecimy po lambda oraz dla danego z
                {
                    k[ll, nn] = (Math.PI / lambda.ElementAt(ll)) * delta_n * apodyzacja.ElementAt(nn);
                    delta[ll, nn] = 2 * Math.PI * neff * ((1 / lambda.ElementAt(ll)) - (1 / lambdaBy.ElementAt(nn)));
                    sigma[ll, nn] = delta[ll, nn]; //chirp
                    k2[ll, nn] = Math.Pow(k[ll, nn], 2);
                    sigma2[ll, nn] = Math.Pow(sigma[ll, nn], 2);
                    if (k2[ll, nn] > sigma[ll, nn])
                    {
                        gammaB[ll, nn] = Math.Pow(k2[ll, nn] - sigma2[ll, nn], 0.5);
                    }
                    else if (k2[ll, nn] < sigma[ll, nn])
                    {
                        gammaB[ll, nn] = Math.Pow(sigma2[ll, nn] - k2[ll, nn], 0.5) *j;
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

            double[,] F11 = new double[ilosc_lambda, ilosc_sekcji];
            double[,] F12 = new double[ilosc_lambda, ilosc_sekcji];
            double[,] F21 = new double[ilosc_lambda, ilosc_sekcji];
            double[,] F22 = new double[ilosc_lambda, ilosc_sekcji];

            for (int ll = 0; ll < ilosc_lambda; ll++)
            {
                for (int nn = 0; nn < ilosc_lambda; nn++) //w pętli lecimy po lambda oraz dla danego z
                {
                    F11[ll,nn] = Math.Cosh(gammaB[ll, nn] * lj.ElementAt(nn)) + j * (sigma[ll, nn] / gammaB[ll, nn]) * Math.Sinh(gammaB[ll, nn] * lj.ElementAt(nn));
                    F12[ll,nn] = j * (k[ll, nn] / gammaB[ll, nn]) * Math.Sinh(gammaB[ll, nn] * lj.ElementAt(nn));
                    F21[ll, nn] = F12[ll, nn];
                    F22[ll, nn] = F11[ll, nn];
                }
            }


            double[,,,] D = new double[2, 2, ilosc_lambda, ilosc_sekcji];
            for (int c = 0; c < ilosc_lambda; c++)
            {
                for (int d = 0; d < ilosc_lambda; d++)
                {
                    D[0, 0, c, d] = F11[c, d];
                    D[0, 1, c, d] = F12[c, d];
                    D[1, 0, c, d] = F21[c, d];
                    D[1, 1, c, d] = F22[c, d];
                }
            }
            


        }
        //private double F11(double c, double d)
        //{

        //}
        //private double F12(double c, double d)
        //{

        //}
        //private double F21(double c, double d)
        //{

        //}
        //private double F22(double c, double d)
        //{

        //}

    }
}
