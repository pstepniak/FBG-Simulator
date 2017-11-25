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
            int ilosc_sekcji = x.Length;
            double t = 1 / ilosc_sekcji;

            /*lj = (t:t: 1)*L;*/
            List<double> lj = new List<double>();
            for (double i = t; i <= 1; i = i + t)
            {
                lj.Add(i * L);
            }

            List<double> okresy = new List<double>();
            List<double> lambdaBy = new List<double>();
            List<double> apodyzacja = new List<double>();
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

        }

    }
}
