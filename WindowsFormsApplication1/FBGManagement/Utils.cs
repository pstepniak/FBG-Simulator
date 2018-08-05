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
        /// <summary>
        /// Zwraca najmniejszą wartość (dynamika piku) w danej tablicy.
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <param name="wavelengths"></param>
        /// <returns></returns>
        public static decimal CalculateCentralWavelenght (List<decimal> simulationResult, List<decimal> wavelengths)
        {
            return wavelengths.ElementAt(Utils.CalculateMinCentralIndex(simulationResult));
        }

        public static decimal CalculateDynamics (List<decimal> simulationResult)
        {
            return simulationResult.ElementAt(Utils.CalculateMinCentralIndex(simulationResult));
        }
        /// <summary>
        /// Zwraca index, pod którym znajduje się minimalna wartość w tablicy (indeks piku na wykresie)
        /// </summary>
        /// <param name="simulationResult">Tablica wynikowa</param>
        /// <returns></returns>
        private static int CalculateMinCentralIndex(List<decimal> simulationResult)
        {
            decimal minValue = 10;
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
        /// <summary>
        /// Zwraca indeks, pod którym znajduje się największa wartość w danej tablicy, w przedziale ograniczonym przez parametry minIndex i maxIndex.
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <param name="minIndex"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Zwraca indeks, pod którym znajduje się największa wartość z lewej strony piku taka, dla której funkcja w przedziale od tej wartości
        /// do piku jest malejąca. Od tej wartości powinna być liczona lewostronna wyniosłość względna piku.
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <returns></returns>
         
        private static int CalculateLeftMaxIndex(List<decimal> simulationResult)
        {
            decimal currentValue = simulationResult.ElementAt(Utils.CalculateMinCentralIndex(simulationResult)); //najmniejsza wartość (wartość piku)
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
        /// <summary>
        /// Zwraca indeks, pod którym znajduje się największa wartość z prawej strony piku taka, dla której funkcja w przedziale od piku do tej wartości jest rosnąca.
        /// Od tej wartości powinna być liczona prawostronna wyniosłość względna piku.
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <returns></returns>
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

            //decimal minValueDistance = 10;
            decimal tempLeftWavelength = wavelengths.ElementAt(0); //długość fali (wartość) wyliczona dokładnie w połowie dynamiki piku
            decimal centralValue; //wartość, dla której indeksu szukamy
            decimal tempValue = leftMaxValue; //wartość zmieniająca się w obiegu pętli dla bieżącego obiegu
            decimal tempPrevValue = leftMaxValue;
            for (int i = leftMaxIndex; i <= centralIndex; i++)
            {
                centralValue = (leftMaxValue - MinValue) / 2 + MinValue;
                tempPrevValue = tempValue;
                tempValue = simulationResult.ElementAt(i);
                if (tempValue == centralValue)
                {
                    //trzeba znaleźć długość fali pod indeksem i

                    tempLeftWavelength = wavelengths.ElementAt(i);
                    break;
                }
                else if (tempValue < centralValue) // tempValue jest na lewo od szukanego punktu większa od szukanej wartości, więc jak będzie mniejsza to obliczamy i przerywamy
                {
                    if (tempValue != centralValue)
                    { //stosujemy aproksymację linią prostą
                        tempLeftWavelength = Math.Abs(wavelengths.ElementAt(i) - wavelengths.ElementAt(i - 1)) * Math.Abs(tempPrevValue - centralValue) / Math.Abs(tempPrevValue - tempValue) + wavelengths.ElementAt(i - 1);
                    }
                    else //jak jest bardzo mała rozdzielczość, to nie będziemy mieć wartości poprzedniej bo ograniczony zakres może się składać z trzech punktów. W takim wypadku zakładamy że punkt wypadł w połowie nieznanego przedziału
                    {
                        tempLeftWavelength = (wavelengths.ElementAt(i) - wavelengths.ElementAt(i - 1)) / 2 + wavelengths.ElementAt(i - 1);
                    }
                    break;
                }
                //if (Math.Abs(centralValue - tempValue) < minValueDistance)
                //{
                //    minValueDistance = Math.Abs((leftMaxValue - MinValue) / 2 - simulationResult.ElementAt(i));
                //    tempLeftWavelength = wavelengths.ElementAt(i);
                //}
            }
            //minValueDistance = 10;
            tempValue = MinValue;
            tempPrevValue = MinValue;
            decimal tempRightWavelength = wavelengths.ElementAt(simulationResult.Count - 1);
            for (int i = centralIndex; i <= rightMaxIndex; i++)
            {
                centralValue = (rightMaxValue - MinValue) / 2 + MinValue;
                tempPrevValue = tempValue;
                tempValue = simulationResult.ElementAt(i);
                if (tempValue == centralValue)
                {
                    //trzeba znaleźć długość fali pod indeksem i

                    tempRightWavelength = wavelengths.ElementAt(i);
                    break;
                }
                else if (tempValue > centralValue) // tempValue jest na lewo od szukanego punktu mniejsza od szukanej wartości, więc jak będzie większa to obliczamy i przerywamy
                {
                    if (tempValue != centralValue)
                    { //stosujemy aproksymację linią prostą
                        tempRightWavelength = Math.Abs(wavelengths.ElementAt(i) - wavelengths.ElementAt(i - 1)) * Math.Abs(tempPrevValue - centralValue) / Math.Abs(tempPrevValue - tempValue) + wavelengths.ElementAt(i - 1);
                    }
                    else //jak jest bardzo mała rozdzielczość, to nie będziemy mieć wartości poprzedniej bo ograniczony zakres może się składać z trzech punktów. W takim wypadku zakładamy że punkt wypadł w połowie nieznanego przedziału
                    {
                        tempRightWavelength = (wavelengths.ElementAt(i) - wavelengths.ElementAt(i - 1)) / 2 + wavelengths.ElementAt(i - 1);
                    }
                    break;
                }
                //if (Math.Abs((rightMaxValue - MinValue) / 2 - simulationResult.ElementAt(i)) < minValueDistance)
                //{
                //    minValueDistance = Math.Abs((rightMaxValue - MinValue) / 2 - simulationResult.ElementAt(i));
                //    tempRightWavelength = wavelengths.ElementAt(i);
                //}
            }

            return tempRightWavelength - tempLeftWavelength;
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
