using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsFormsApplication1.FBGManagement;

namespace WindowsFormsApplication1
{
    public partial class FBGForm : Form
    {
        public FBGForm()
        {
            InitializeComponent();
        }

        private void FBGForm_Load(object sender, EventArgs e)
        {
            //chartReflection.Series["Reflection"].Points.AddXY(1300, 1);
            //chartReflection.Series["Reflection"].Points.AddXY(1425, 1);
            //chartReflection.Series["Reflection"].Points.AddXY(1430, 0.98);
            //chartReflection.Series["Reflection"].Points.AddXY(1435, 0.95);
            //chartReflection.Series["Reflection"].Points.AddXY(1440, 0.95);
            //chartReflection.Series["Reflection"].Points.AddXY(1445, 0.96);
            //chartReflection.Series["Reflection"].Points.AddXY(1450, 0.97);
            //chartReflection.Series["Reflection"].Points.AddXY(1455, 0.99);
            //chartReflection.Series["Reflection"].Points.AddXY(1500, 0.99);
            //chartReflection.Series["Reflection"].Points.AddXY(1510, 0.97);
            //chartReflection.Series["Reflection"].Points.AddXY(1520, 0.92);
            //chartReflection.Series["Reflection"].Points.AddXY(1530, 0.83);
            //chartReflection.Series["Reflection"].Points.AddXY(1540, 0.72);
            //chartReflection.Series["Reflection"].Points.AddXY(1550, 0.57);
            //chartReflection.Series["Reflection"].Points.AddXY(1560, 0.48);
            //chartReflection.Series["Reflection"].Points.AddXY(1570, 0.42);
            //chartReflection.Series["Reflection"].Points.AddXY(1580, 0.39);
            //chartReflection.Series["Reflection"].Points.AddXY(1590, 0.37);
            //chartReflection.Series["Reflection"].Points.AddXY(1600, 0.36);
            //chartReflection.Series["Reflection"].Points.AddXY(1610, 0.37);
            //chartReflection.Series["Reflection"].Points.AddXY(1620, 0.4);
            //chartReflection.Series["Reflection"].Points.AddXY(1630, 0.44);
            //chartReflection.Series["Reflection"].Points.AddXY(1640, 0.5);
            //chartReflection.Series["Reflection"].Points.AddXY(1650, 0.6);
            //chartReflection.Series["Reflection"].Points.AddXY(1660, 0.74);
            //chartReflection.Series["Reflection"].Points.AddXY(1670, 0.84);
            //chartReflection.Series["Reflection"].Points.AddXY(1680, 0.92);
            //chartReflection.Series["Reflection"].Points.AddXY(1690, 0.97);
            //chartReflection.Series["Reflection"].Points.AddXY(1700, 1);
            //chartReflection.Series["Reflection"].Points.AddXY(1900, 1);

            //foreach (DataPoint p in chartReflection.Series["Reflection"].Points)
            //{
            //    double x = p.YValues.First();
            //    Random rnd = new Random();
            //    double r = (rnd.Next(1, 10))/10;
            //    p.SetValueY(x - r);
            //}

            int countOfProbe = 500;
            //double minimalWavelength = 1530.75;
            //double maximalWavelength = 1533.75;
            double minimalWavelength = 1525;
            double maximalWavelength = 1535;
            Symulation symulation = new Symulation(countOfProbe, minimalWavelength, maximalWavelength);
            double neff = 1.44688; //efektywny współczynnik załamania
            double L = 10000 * Math.Pow(10, -6); //długość siatki
            double lambdaB = 1531.2 * Math.Pow(10, -9); //długość fali Bragga
            double okres = lambdaB / (2 * Math.PI * neff);
            double delta_n = 0.00010; //delta n
            Grating grating = new Grating(okres, L, delta_n, neff);


            List<double> Ry = symulation.Symulate(grating);
            for (int i = 0; i < countOfProbe; i++)
            {
                chartReflection.Series["Reflection"].Points.AddXY(minimalWavelength+(i+1)*((maximalWavelength-minimalWavelength)/countOfProbe), Ry.ElementAt(i));
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
