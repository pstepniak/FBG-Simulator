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
            //inicjowanie formularza
            tb_CountOfProbe.Text = "1000";

            tb_MinimalWavelength.Text = "1530,75";
            tb_maximalWavelength.Text = "1533,75";

            tb_Grating_NEff.Text = "1,4469";
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
            decimal minimalWavelength = 1525;
            decimal maximalWavelength = 1535;
            /*wczytanie z interfejsu parametrów symulacji*/
            if (!String.IsNullOrEmpty(tb_CountOfProbe.Text))
                countOfProbe = Int32.Parse(tb_CountOfProbe.Text);
            if (!String.IsNullOrEmpty(tb_MinimalWavelength.Text))
                minimalWavelength = Decimal.Parse(tb_MinimalWavelength.Text);
            if (!String.IsNullOrEmpty(tb_maximalWavelength.Text))
                maximalWavelength = Decimal.Parse(tb_maximalWavelength.Text);
            Simulation symulation = new Simulation(countOfProbe, minimalWavelength, maximalWavelength);
            /*domyślne dane siatki*/
            //double neff = 1.44688; //efektywny współczynnik załamania
            decimal neff = 1.4469m; //efektywny współczynnik załamania
            decimal L = 10000m * (decimal)Math.Pow(10, -6); //długość siatki
            decimal lambdaB = 1531.2m * (decimal)Math.Pow(10, -9); //długość fali Bragga
            decimal okres = lambdaB / (2 * (decimal)Math.PI * neff);
            decimal delta_n = 0.00010m; //delta n
            int parts = 10;
            if (!String.IsNullOrEmpty(tb_Grating_NEff.Text))
                neff = Decimal.Parse(tb_Grating_NEff.Text);
            if (!String.IsNullOrEmpty(tb_GratingRIM.Text))
                delta_n = Decimal.Parse(tb_GratingRIM.Text);
            if (!String.IsNullOrEmpty(tbGratingPeriod.Text))
                okres = Decimal.Parse(tbGratingPeriod.Text) * (decimal)Math.Pow(10, -9); //168
            if (!String.IsNullOrEmpty(tb_GratingLength.Text))
                L = Decimal.Parse(tb_GratingLength.Text)* (decimal)Math.Pow(10, -6);
            if (!String.IsNullOrEmpty(tb_GratingParts.Text))
                parts = Int32.Parse(tb_GratingParts.Text);
            /*wczytanie z interfejsu właściwości siatki*/

            //Grating grating = new Grating(okres, L, delta_n, neff);
            Grating grating = new Grating(okres, L, delta_n, neff, parts);


            List<decimal> Ry = symulation.Simulate(grating);
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
