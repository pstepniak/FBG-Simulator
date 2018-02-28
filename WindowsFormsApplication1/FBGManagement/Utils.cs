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
    }
}
