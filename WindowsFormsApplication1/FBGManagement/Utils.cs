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
            decimal previousValue = simulationResult.ElementAt(Utils.CalculateMinCentralIndex(simulationResult) + Utils.CalculateMinCentralIndex(simulationResult)== simulationResult.Count-1?0:1);
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
            decimal previousValue = simulationResult.ElementAt(Utils.CalculateMinCentralIndex(simulationResult)==0?1:Utils.CalculateMinCentralIndex(simulationResult)- 1);
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
            }

            return tempRightWavelength - tempLeftWavelength;
        }
        /// <summary>
        /// Zwraca indeks, pod którym w tablicach wynikowych znajdują się wartości pierwszego piku na lewo od głównego piku.
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <param name="wavelengths"></param>
        /// <returns></returns>
        private static int CalculateLeftPeakIndex(List<decimal> simulationResult)
        {
            int leftMaxIndex = CalculateLeftMaxIndex(simulationResult);
            decimal maxValue = simulationResult.ElementAt(leftMaxIndex);
            decimal tempValue = simulationResult.ElementAt(leftMaxIndex);

            for (int i = leftMaxIndex; i>=0;--i)
            {
                if (simulationResult.ElementAt(i) > tempValue)
                {
                    return i + 1;
                }
                tempValue = simulationResult.ElementAt(i);
            }
            return 0;
        }
        /// <summary>
        /// Zwraca indeks, pod którym w tablicach wynikowych znajdują się wartości pierwszego piku na prawo od głównego piku.
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <returns></returns>
        private static int CalculateRightPeakIndex(List<decimal> simulationResult)
        {
            int rightMaxIndex = CalculateRightMaxIndex(simulationResult);
            decimal maxValue = simulationResult.ElementAt(rightMaxIndex);
            decimal tempValue = simulationResult.ElementAt(rightMaxIndex);

            for (int i = rightMaxIndex;i<simulationResult.Count;++i)
            {
                if (simulationResult.ElementAt(i) > tempValue)
                {
                    return i - 1;
                }
                tempValue = simulationResult.ElementAt(i);
            }
            return simulationResult.Count - 1;
        }
        /// <summary>
        /// Zwraca dynamikę piku na lewo od piku głównego
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <returns></returns>
        private static decimal CalculateLeftPeakDynamics(List<decimal> simulationResult)
        {
            return simulationResult.ElementAt(Utils.CalculateLeftPeakIndex(simulationResult));
        }
        /// <summary>
        /// Zwraca dynamikę piku na prawo od piku głównego
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <returns></returns>
        private static decimal CalculateRightPeakDynamics(List<decimal> simulationResult)
        {
            return simulationResult.ElementAt(Utils.CalculateRightPeakIndex(simulationResult));
        }
        /// <summary>
        /// Zwraca długość fali piku na lewo od piku głównego
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <param name="wavelengths"></param>
        /// <returns></returns>
        private static decimal CalculateLeftPeakWavelenght(List<decimal> simulationResult, List<decimal> wavelengths)
        {
            return wavelengths.ElementAt(Utils.CalculateLeftPeakIndex(simulationResult));
        }
        /// <summary>
        /// Zwraca długość fali piku na prawo od piku głównego
        /// </summary>
        /// <param name="simulationResult"></param>
        /// <param name="wavelengths"></param>
        /// <returns></returns>
        private static decimal CalculateRightPeakWavelength(List<decimal> simulationResult, List<decimal> wavelengths)
        {
            return wavelengths.ElementAt(Utils.CalculateRightPeakIndex(simulationResult));
        }
        public static decimal CalculateAdjacentDynamic(List<decimal> simulationResult, List<decimal> wavelengths)
        {
            return Math.Min(CalculateLeftPeakDynamics(simulationResult), CalculateRightPeakDynamics(simulationResult));
        }
        public static List<decimal> ConvertToDecimal(List<string> list)
        {
            List<decimal> result = new List<decimal>();

            foreach(string s in list)
            {
                result.Add(Decimal.Parse(s.Replace('.', ','), System.Globalization.NumberStyles.Float));
            }
            return result;
        }
        public static List<decimal> TransformDecimalListToNormalizedList(List<decimal> list)
        {
            List<decimal> result = new List<decimal>();
            decimal maxValue = list.First();
            foreach (decimal item in list)
            {
                if (item > maxValue)
                {
                    maxValue = item;
                }
            }
            decimal tmpValue;
            foreach (decimal item in list)
            {
                tmpValue = item / maxValue;
                if (tmpValue > 1)
                {
                    tmpValue = 1;
                }
                result.Add(tmpValue);
            }
            return result;
        }
        public static List<decimal> TransformDecimalListToSkippedInputDifferencesList(List<decimal> list)
        {
            //metoda neutralizuje krzywą charakterystykę źródła. Wzory wynikają ze schematu narysowanego na kartce, mogącego być odtworzonym na podstawie poniższego opisu.

            //list jest listą wartości transmisji na pojedynczych długościach fali.
            //a jest rozbieżnością między najniższą a najwyższą wartością transmitowanej energii dla 100% mocy transmisyjnej (w zasadzie między ostatnią, a pierwszą).
            //b jest zakresem okna, ale w poniższym algorytmie przyjęto, że jest to długość listy (założenie prawdziwe, jeśli próbki są co równą długość fali).
            //c jest szukaną wartością odniesienia 100% energii transmisyjnej w transformowanym punkcie.
            //d jest odległością od końca okna (końca listy) do bieżącej pozycji (długości fali - w uproszczeniu indeksu na liście dla transformowanego elementu).
            //oznaczając na charakterystyce transmisyjnej poszczególne wartości można z twierdzenia Talesa otrzymać: c=d*a/b

            //wartość transmisji dana w transformowanym punkcie (val) jest odniesiona do wartości c. Chcemy odnieść ją (szukana x) do wartości maksymalnej transmisji (max).
            //zatem z proporcji układamy: x=val*max/(max-c) // max - c , gdyż c jest szukanym dopełnieniem 100% transmisji w szukanym punkcie.
            //dwa powyższe wzory stanowią podstawę działania tej metody.
            List<decimal> result = new List<decimal>();
            decimal a = list.Last() - list.First(); //zakładamy, że na pierwszym i ostatnim elemencie listy mamy 100% mocy transmisyjnej dostępnej na danej długości fali.
            decimal b = list.Count() - 1;
            decimal max = list.Max(); //w związku z powyższym założeniem max występuje na początku lub na końcu listy
            decimal d;
            decimal val;
            decimal x;
            decimal c;

            if (a == 0) return list;
            if (a < 0) a = -1 * a;

            for (int i = 0; i < list.Count; ++i)
            {
                if (list.Last() > list.First()) //profil rosnący
                {
                    d = b - i;
                }
                else //profil malejący
                {
                    d = i;
                }
                val = list.ElementAt(i);
                if (d != 0)
                {
                    c = d * a / b;
                    x = val * max / (max - c);
                }
                else
                {
                    x = max;
                }
                result.Add(x);
            }

            return result;
        }


        public static void CopyToClipboard(string textToSave, bool removeUnits = false)
        {
            if (removeUnits)
            {
                textToSave = textToSave.Replace(" um", "");
            }
            System.Windows.Forms.Clipboard.SetText(textToSave);
        }
    }
}
