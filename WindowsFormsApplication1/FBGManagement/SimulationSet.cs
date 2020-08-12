using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WindowsFormsApplication1.FBGManagement
{
    public enum VariableProperty
    {
        [Description("Length")]
        Length,
        [Description("Period")]
        Period,
        [Description("Refractive Index Modulation")]
        RefractiveIndexModulation,
        [Description("Refractive Index Eff")]
        RefractiveIndexEff,
        [Description("Apodisation Param")]
        ApodisationParam,
        [Description("Chirp Param")]
        ChirpParam,
        [Description("Chirp Min Period Factor")]
        ChirpMinPeriodFactor,
        Undefined
    }
    class SimulationSet
    {
        

        public static void FillVariablePropertyCombo(System.Windows.Forms.ComboBox combo)
        {
            foreach (VariableProperty varProp in (VariableProperty[]) Enum.GetValues(typeof(VariableProperty)))
            {
                string description = varProp.Description();
                if (!description.Equals("Undefined"))
                {
                    combo.Items.Add(description);
                }
            }
        }

        public static VariableProperty GetVariablePropertyEnum(string description)
        {
            foreach (VariableProperty varProp in (VariableProperty[])Enum.GetValues(typeof(VariableProperty)))
            {
                if (varProp.Description().Equals(description))
                {
                    return varProp;
                }
            }
            return VariableProperty.Undefined;
        }

        private Simulation simulation;
        private Grating referenceGrating;
        private VariableProperties variableProperties;
        public SimulationSet(Simulation simulation, Grating referenceGrating, VariableProperties variableProperties)
        {
            this.simulation = simulation;
            this.referenceGrating = referenceGrating;
            this.variableProperties = variableProperties;
        }
        public SimulationSetResult Simulate()
        {
            Grating grating = referenceGrating.Copy();
            List<decimal> wavelenghts = new List<decimal>();
            List<decimal> singleSimulationResult = new List<decimal>();
            List<decimal> varialePropertyValues = new List<decimal>();
            List<TransmissionCharacteristicsProperties> transmissionCharacteristicsProperty = new List<TransmissionCharacteristicsProperties>();


            for (decimal variablePropertyValue = variableProperties.valueFrom; variablePropertyValue <= variableProperties.valueTo; variablePropertyValue += variableProperties.step)
            {
                simulation.Clear();
                grating.SetVariableProperty(variableProperties.variableProperty, variablePropertyValue);
                singleSimulationResult = simulation.Simulate(grating, out wavelenghts);
                decimal centralWavelength = Utils.CalculateCentralWavelenght(singleSimulationResult, wavelenghts);
                decimal dynamics = Utils.CalculateDynamics(singleSimulationResult);
                decimal fwhm = Utils.CalculateFWHM(singleSimulationResult, wavelenghts);
                decimal adjacentDynamics = Utils.CalculateAdjacentDynamic(singleSimulationResult, wavelenghts);
                transmissionCharacteristicsProperty.Add(new TransmissionCharacteristicsProperties { adjacentDynamics = adjacentDynamics, centralWavelength = centralWavelength, dynamics = dynamics, fwhm = fwhm });
                varialePropertyValues.Add(variablePropertyValue);
            }
            return new SimulationSetResult { transmissionCharacteristicProperties = transmissionCharacteristicsProperty, gratingVariablePropertyValues = varialePropertyValues, variableProperty = variableProperties.variableProperty };
        }
        public void PrintTransientCharacteristics(System.Windows.Forms.DataVisualization.Charting.Chart chart, SimulationSetResult simulationSetResult)
        {
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }
            for (int i = 0; i < simulationSetResult.transmissionCharacteristicProperties.Count; i++)
            {
                //chart.Series["centralWavelenght"].Points.AddXY(simulationSetResult.gratingVariablePropertyValues.ElementAt(i), simulationSetResult.transmissionCharacteristicProperties.ElementAt(i).centralWavelength);
                chart.Series["dynamics"].Points.AddXY(simulationSetResult.gratingVariablePropertyValues.ElementAt(i), simulationSetResult.transmissionCharacteristicProperties.ElementAt(i).dynamics);
                //chart.Series["fwhm"].Points.AddXY(simulationSetResult.gratingVariablePropertyValues.ElementAt(i), simulationSetResult.transmissionCharacteristicProperties.ElementAt(i).fwhm);
                //chart.Series["adjacentDynamics"].Points.AddXY(simulationSetResult.gratingVariablePropertyValues.ElementAt(i), simulationSetResult.transmissionCharacteristicProperties.ElementAt(i).adjacentDynamics);
            }
            chart.ChartAreas[0].AxisY.Minimum = (double)simulationSetResult.transmissionCharacteristicProperties.Min(x => x.dynamics);
            chart.ChartAreas[0].AxisY.Maximum = (double)simulationSetResult.transmissionCharacteristicProperties.Max(x => x.dynamics);
        }
    }
    public struct SimulationSetResult
    {
        public List<TransmissionCharacteristicsProperties> transmissionCharacteristicProperties;
        public List<decimal> gratingVariablePropertyValues;
        public VariableProperty variableProperty;
    }
    public struct VariableProperties {
        public VariableProperty variableProperty;
        public decimal valueFrom;
        public decimal valueTo;
        public decimal step;
    }
    public struct TransmissionCharacteristicsProperties
    {
        public decimal centralWavelength;
        public decimal dynamics;
        public decimal fwhm;
        public decimal adjacentDynamics;
    }
    public static class ReflectionHelpers
    {
        public static string GetCustomDescription(object objEnum)
        {
            var fi = objEnum.GetType().GetField(objEnum.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : objEnum.ToString();
        }

        public static string Description(this Enum value)
        {
            return GetCustomDescription(value);
        }
    }
}
