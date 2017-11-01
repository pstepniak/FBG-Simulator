using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FBGManagement
{
    class Symulation
    {
        public Symulation() {

        }
        public void Symulate()
        {
            double[] x =[168.43, 168.43, 168.43, 168.43, 168.43, 168.43, 168.43, 168.43];
            int ilosc_lambda = 1000;
            double s = 1530.75 + (3 / ilosc_lambda);
            int ilosc_sekcji = x.Length;
            double t = 1 / ilosc_sekcji;
        }

    }
}
