using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FBGManagement
{
    class Utils
    {
        public static void SaveArrayAsCSV(Array arrayToSave, string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                WriteItemsToFile(arrayToSave, file);
            }
        }

        private static void WriteItemsToFile(Array items, TextWriter file)
        {
            foreach (object item in items)
            {
                if (item is Array)
                {
                    WriteItemsToFile(item as Array, file);
                    file.Write(Environment.NewLine);
                }
                else file.Write(item + ";");
            }
        }
        //private static void Save2DArrayAsCSV(Array arrayToSave, string fileName)
        //{
        //    using (StreamWriter outfile = new StreamWriter(fileName))
        //    {
        //        for (int x = 0; x < data.Length; x++)
        //        {
        //            string content = "";
        //            for (int y = 0; y < data[x].Length; y++)
        //            {
        //                content += data[x][y].ToString() + ",";
        //            }
        //            //trying to write data to csv
        //            outfile.WriteLine(content);
        //        }


        //    }
        //}
        public static decimal CalculateCentralWavelenght (List<decimal> simulationResult, List<decimal> wavelengths)
        {
            return wavelengths.ElementAt(Utils.CalculateMinCentralIndex(simulationResult));
        }
        public static decimal CalculateDynamics (List<decimal> simulationResult)
        {
            return simulationResult.ElementAt(Utils.CalculateMinCentralIndex(simulationResult));
        }
        private static int CalculateMinCentralIndex(List<decimal> simulationResult)
        {
            decimal minValue = 1;
            int minIndex = -1;
            for (int i = 0; i < simulationResult.Count; i++)
            {
                if (simulationResult.ElementAt(i) < minValue)
                {
                    minValue = simulationResult.ElementAt(i);
                    minIndex = i;
                }
            }
            return minIndex;
        }
        private static int CalculateMaxCentralIndex(List<decimal> simulationResult, int minIndex, int maxIndex)
        {
            decimal maxValue = 0;
            for (int i = minIndex; i < maxIndex; i++)
            {
                if (simulationResult.ElementAt(i) > maxValue)
                {
                    maxValue = simulationResult.ElementAt(i);
                    minIndex = i;
                }
            }
            return minIndex;
        }
        private static int CalculateLeftMaxIndex(List<decimal> simulationResult)
        {
            decimal currentValue = simulationResult.ElementAt(Utils.CalculateMinCentralIndex(simulationResult));
            decimal previousValue = simulationResult.ElementAt(Utils.CalculateMinCentralIndex(simulationResult) + 1);
            for (int i = Utils.CalculateMinCentralIndex(simulationResult)-1; i > 0; i--)
            {
                currentValue = simulationResult.ElementAt(i);
                previousValue = simulationResult.ElementAt(i + 1);
                if (previousValue > currentValue)
                {
                    return i + 1;
                }
            }
            return 0;
        }
        private static int CalculateRightMaxIndex(List<decimal> simulationResult)
        {
            decimal currentValue = simulationResult.ElementAt(Utils.CalculateMinCentralIndex(simulationResult));
            decimal previousValue = simulationResult.ElementAt(Utils.CalculateMinCentralIndex(simulationResult)-1);
            for (int i = Utils.CalculateMinCentralIndex(simulationResult)+1; i < simulationResult.Count; i++)
            {
                currentValue = simulationResult.ElementAt(i);
                previousValue = simulationResult.ElementAt(i-1);
                if (previousValue>currentValue)
                {
                    return i - 1;
                }
            }
            return simulationResult.Count - 1;
        }
        
        public static decimal CalculateFWHM(List<decimal> simulationResult, List<decimal> wavelengths)
        {
            int leftMaxIndex = CalculateLeftMaxIndex(simulationResult);
            int rightMaxIndex = CalculateRightMaxIndex(simulationResult);
            int centralIndex = CalculateMinCentralIndex(simulationResult);
            decimal leftMaxValue = simulationResult.ElementAt(leftMaxIndex);
            decimal rightMaxValue = simulationResult.ElementAt(rightMaxIndex);
            decimal MinValue = simulationResult.ElementAt(centralIndex);

            decimal minValueDistance = 1;
            decimal leftWavelength = wavelengths.ElementAt(0);
            for (int i = leftMaxIndex; i < centralIndex; i++)
            {
                if (Math.Abs((leftMaxValue- MinValue)/2 - simulationResult.ElementAt(i)) < minValueDistance)
                {
                    minValueDistance = Math.Abs((leftMaxValue - MinValue) / 2 - simulationResult.ElementAt(i));
                    leftWavelength = wavelengths.ElementAt(i);
                }
            }
            minValueDistance = 1;
            decimal rightWavelength = wavelengths.ElementAt(simulationResult.Count - 1);
            for (int i = centralIndex; i < rightMaxIndex; i++)
            {
                if (Math.Abs((rightMaxValue - MinValue) / 2 - simulationResult.ElementAt(i)) < minValueDistance)
                {
                    minValueDistance = Math.Abs((rightMaxValue - MinValue) / 2 - simulationResult.ElementAt(i));
                    rightWavelength = wavelengths.ElementAt(i);
                }
            }

            return rightWavelength - leftWavelength;
        }
        private static decimal CalculateLeftWavelength(List<decimal> simulationResult, List<decimal> wavelengths)
        {
            int leftMaxIndex = CalculateLeftMaxIndex(simulationResult);
            return wavelengths.ElementAt(Utils.CalculateMaxCentralIndex(simulationResult, 0, leftMaxIndex));
        }
        private static decimal CalculateRightWavelength(List<decimal> simulationResult, List<decimal> wavelengths)
        {
            int rightMaxIndex = CalculateRightMaxIndex(simulationResult);
            return wavelengths.ElementAt(Utils.CalculateMaxCentralIndex(simulationResult, rightMaxIndex, simulationResult.Count-1));
        }
        public static decimal CalculateAdjacentWavelength(List<decimal> simulationResult, List<decimal> wavelengths)
        {
            return Math.Max(CalculateLeftWavelength(simulationResult, wavelengths), CalculateRightWavelength(simulationResult, wavelengths));
        }
    }
}
