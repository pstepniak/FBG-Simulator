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
            tb_CountOfProbe.Text = "1000";
            //tb_MinimalWavelength.Text = "1525";
            //tb_maximalWavelength.Text = "1535";

            tb_MinimalWavelength.Text = "1530,75";
            tb_maximalWavelength.Text = "1533,75";

            tb_Grating_NEff.Text = "1,44688";
            tb_GratingRIM.Text = "0,0001";
            tbGratingPeriod.Text = "168,43";
            tb_GratingLength.Text = "10000";
            //tb_GratingParts.Text = "10";
            tb_GratingParts.Text = "1";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            /*czyszczenie wykresów*/
            foreach (var series in chartReflection.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartTransmission.Series)
            {
                series.Points.Clear();
            }
            /*domyślne wartości symulacji*/
            int countOfProbe = 500;
            double minimalWavelength = 1525;
            double maximalWavelength = 1535;
            /*wczytanie z interfejsu parametrów symulacji*/
            if (!String.IsNullOrEmpty(tb_CountOfProbe.Text))
                countOfProbe = Int32.Parse(tb_CountOfProbe.Text);
            if (!String.IsNullOrEmpty(tb_MinimalWavelength.Text))
                minimalWavelength = Double.Parse(tb_MinimalWavelength.Text);
            if (!String.IsNullOrEmpty(tb_maximalWavelength.Text))
                maximalWavelength = Double.Parse(tb_maximalWavelength.Text);
            Simulation symulation = new Simulation(countOfProbe, minimalWavelength, maximalWavelength);
            /*domyślne dane siatki*/
            //double neff = 1.44688; //efektywny współczynnik załamania
            double neff = 1.4469; //efektywny współczynnik załamania
            double L = 10000 * Math.Pow(10, -6); //długość siatki
            double lambdaB = 1531.2 * Math.Pow(10, -9); //długość fali Bragga
            double okres = lambdaB / (2 * Math.PI * neff);
            double delta_n = 0.00010; //delta n
            int parts = 10;
            if (!String.IsNullOrEmpty(tb_Grating_NEff.Text))
                neff = Double.Parse(tb_Grating_NEff.Text);
            if (!String.IsNullOrEmpty(tb_GratingRIM.Text))
                delta_n = Double.Parse(tb_GratingRIM.Text);
            if (!String.IsNullOrEmpty(tbGratingPeriod.Text))
                okres = Double.Parse(tbGratingPeriod.Text) * Math.Pow(10, -9); //168
            if (!String.IsNullOrEmpty(tb_GratingLength.Text))
                L = Double.Parse(tb_GratingLength.Text)* Math.Pow(10, -6);
            if (!String.IsNullOrEmpty(tb_GratingParts.Text))
                parts = Int32.Parse(tb_GratingParts.Text);
            /*wczytanie z interfejsu właściwości siatki*/

            //Grating grating = new Grating(okres, L, delta_n, neff);
            Grating grating = new Grating(okres, L, delta_n, neff, parts);


            List<double> Ry = symulation.Simulate(grating);
            for (int i = 0; i < countOfProbe; i++)
            {
                chartTransmission.Series["Transmission"].Points.AddXY(minimalWavelength + (i + 1) * ((maximalWavelength - minimalWavelength) / countOfProbe), Ry.ElementAt(i));
                chartReflection.Series["Reflection"].Points.AddXY(minimalWavelength + (i + 1) * ((maximalWavelength - minimalWavelength) / countOfProbe), 1-Ry.ElementAt(i));
            }
            //zapis wyniku do pliku
            Utils.SaveArrayAsCSV(Ry.ToArray(), "C:\\FBG\\x.csv");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
