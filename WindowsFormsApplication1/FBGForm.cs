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

            //dane symulacji
            tb_CountOfProbe.Text = "1000";
            tb_MinimalWavelength.Text = "1530,75";
            tb_maximalWavelength.Text = "1533,75";
            //dane siatki
            tb_Grating_NEff.Text = "1,4469";
            tb_GratingRIM.Text = "0,0001";
            tbGratingPeriod.Text = "168,43";
            tb_GratingLength.Text = "10000";
            tb_GratingParts.Text = "1";
            //dane apodyzacji
            cb_gratingApodProfile.Items.Add(Grating.Apodisation.Gaussian);
            cb_gratingApodProfile.Items.Add(Grating.Apodisation.Sin);
            cb_gratingApodProfile.Items.Add(Grating.Apodisation.Sinc);
            cb_gratingApodProfile.Items.Add(Grating.Apodisation.None);
            cb_gratingApodProfile.SelectedItem = Grating.Apodisation.None;
            tb_gratingApodParam.Text = "5";
        }
        private Grating PrepareGrating()
        {
            /*domyślne dane siatki*/
            //double neff = 1.44688; //efektywny współczynnik załamania
            decimal neff = 1.4469m; //efektywny współczynnik załamania
            decimal L = 10000m * (decimal)Math.Pow(10, -6); //długość siatki
            decimal lambdaB = 1531.2m * (decimal)Math.Pow(10, -9); //długość fali Bragga
            decimal okres = lambdaB / (2 * (decimal)Math.PI * neff);
            decimal delta_n = 0.00010m; //delta n
            Grating.Apodisation apodisationProfile = Grating.Apodisation.None;
            decimal apodisationParam = 1;
            bool apodisationReverse = false;
            int parts = 10;
            if (!String.IsNullOrEmpty(tb_Grating_NEff.Text))
                neff = Decimal.Parse(tb_Grating_NEff.Text);
            if (!String.IsNullOrEmpty(tb_GratingRIM.Text))
                delta_n = Decimal.Parse(tb_GratingRIM.Text);
            if (!String.IsNullOrEmpty(tbGratingPeriod.Text))
                okres = Decimal.Parse(tbGratingPeriod.Text) * (decimal)Math.Pow(10, -9); //168
            if (!String.IsNullOrEmpty(tb_GratingLength.Text))
                L = Decimal.Parse(tb_GratingLength.Text) * (decimal)Math.Pow(10, -6);
            if (!String.IsNullOrEmpty(tb_GratingParts.Text))
                parts = Int32.Parse(tb_GratingParts.Text);
            /*wczytanie z interfejsu właściwości siatki*/

            if (cb_gratingApodProfile.SelectedItem != null && !String.IsNullOrEmpty(cb_gratingApodProfile.SelectedItem.ToString()))
                apodisationProfile = (Grating.Apodisation)cb_gratingApodProfile.SelectedItem;
            if (!String.IsNullOrEmpty(tb_gratingApodParam.Text))
                apodisationParam = Decimal.Parse(tb_gratingApodParam.Text);
            apodisationReverse = cb_gratingApodReverse.Checked;
            //Grating grating = new Grating(okres, L, delta_n, neff);
            //Grating grating = new Grating(okres, L, delta_n, neff, parts);
            return new Grating(okres, L, delta_n, neff, parts, apodisationProfile, apodisationParam, apodisationReverse);
        }
        private Simulation PrepareSimulation()
        {
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
            return new Simulation(countOfProbe, minimalWavelength, maximalWavelength);
        }
        private void PrintGraphs(Simulation simulation, List<decimal> simulationResult, List<decimal> wavelengths)
        {
            for (int i = 0; i < simulation.countOfProbes; i++)
            {
                chartTransmission.Series["Transmission"].Points.AddXY(simulation.s + (i + 1) * ((simulation.s2 - simulation.s) / simulation.countOfProbes), simulationResult.ElementAt(i));
                chartReflection.Series["Reflection"].Points.AddXY(simulation.s + (i + 1) * ((simulation.s2 - simulation.s) / simulation.countOfProbes), 1 - simulationResult.ElementAt(i));
                //chartTransmission.Series["Transmission"].Points.AddXY(wavelengths.ElementAt(i), Ry.ElementAt(i));
                //chartReflection.Series["Reflection"].Points.AddXY(wavelengths.ElementAt(i), 1 - Ry.ElementAt(i));
            }
        }
        private void PrintGraphs2(Simulation simulation, List<decimal> simulationResult, List<decimal> wavelengths)
        {
            for (int i = 0; i < simulation.countOfProbes; i++)
            {
                chartTransmission.Series["Transmission2"].Points.AddXY(simulation.s + (i + 1) * ((simulation.s2 - simulation.s) / simulation.countOfProbes), simulationResult.ElementAt(i));
                chartReflection.Series["Reflection2"].Points.AddXY(simulation.s + (i + 1) * ((simulation.s2 - simulation.s) / simulation.countOfProbes), 1 - simulationResult.ElementAt(i));
            }
        }
        private void RefreshApodisationGraph()
        {
            try
            {
                Grating grating = PrepareGrating();
                PrintApodisationGraph(grating);
            } catch (Exception e)
            {

            }
        }
        private void PrintApodisationGraph(Grating grating)
        {
            foreach (var series in chartApod.Series)
            {
                series.Points.Clear();
            }
            int probes = 100;

            for (int i = 0; i < probes; i++)
            {
                //wyznaczamy wartość neff uwzględniająz profil apodyzacji
                chartApod.Series["Apodisation"].Points.AddXY(i, grating.ProfileForSection(i, probes) * grating.refractiveIndexModulation + grating.neff);
            }
            chartApod.ChartAreas[0].AxisY.Minimum = (double)grating.neff;
            if (grating.refractiveIndexModulation != 0)
                chartApod.ChartAreas[0].AxisY.Maximum = (double)(grating.neff + grating.refractiveIndexModulation);
            else
                chartApod.ChartAreas[0].AxisY.Maximum = (double)(grating.neff + 0.0001m);
        }
        private void ClearGraphs()
        {
            foreach (var series in chartReflection.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartTransmission.Series)
            {
                series.Points.Clear();
            }
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Simulation simulation = PrepareSimulation();
            Grating grating = PrepareGrating();

            //Wyczyszczenie wykresów
            ClearGraphs();
            //Wykonanie symulacji
            SimulateWithProperMethod(simulation, grating);
        }

        private void SimulateWithProperMethod(Simulation simulation, Grating grating)
        {
            List<decimal> wavelengths = null;
            if (rb_partsInTM.Checked || rb_both.Checked)
            {
                List<decimal> simulationResult = simulation.Simulate(grating, out wavelengths);
                //Narysowanie wykresów
                PrintGraphs(simulation, simulationResult, wavelengths);
                //zapis wyniku do pliku
                Utils.SaveArrayAsCSV(simulationResult.ToArray(), "C:\\FBG\\x.csv");
            }
            if (rb_TMForEach.Checked || rb_both.Checked)
            {
                List<decimal> simulationResult = simulation.SimulateWithDividedGrating(grating, out wavelengths);
                //Narysowanie wykresów
                PrintGraphs2(simulation, simulationResult, wavelengths);
                //zapis wyniku do pliku
                Utils.SaveArrayAsCSV(simulationResult.ToArray(), "C:\\FBG\\x.csv");
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cb_gratingApodProfile_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox s = sender as ComboBox;
            if (s != null)
            {
                if (Grating.Apodisation.Gaussian.Equals(s.SelectedItem))
                {
                    tb_gratingApodParam.Enabled = true;
                    l_grtingApodParam.Enabled = true;
                }
                else
                {
                    tb_gratingApodParam.Enabled = false;
                    l_grtingApodParam.Enabled = false;
                }

                if (Grating.Apodisation.None.Equals(s.SelectedItem))
                {
                    cb_gratingApodReverse.Enabled = false;
                    cb_gratingApodReverse.Checked = false;
                }
                else
                {
                    cb_gratingApodReverse.Enabled = true;
                }
            }
            RefreshApodisationGraph();
        }

        private void tb_gratingApodParam_TextChanged(object sender, EventArgs e)
        {
            RefreshApodisationGraph();
        }

        private void tb_GratingLength_TextChanged(object sender, EventArgs e)
        {
            RefreshApodisationGraph();
        }

        private void tb_GratingRIM_TextChanged(object sender, EventArgs e)
        {
            RefreshApodisationGraph();
        }

        private void tb_Grating_NEff_TextChanged(object sender, EventArgs e)
        {
            RefreshApodisationGraph();
        }

        private void tb_GratingParts_TextChanged(object sender, EventArgs e)
        {
            RefreshApodisationGraph();
        }

        private void cb_gratingApodReverse_CheckedChanged(object sender, EventArgs e)
        {
            RefreshApodisationGraph();
        }
    }
}
