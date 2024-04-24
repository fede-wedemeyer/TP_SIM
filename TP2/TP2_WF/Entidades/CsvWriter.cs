using System;
using System.IO;

namespace TP2_WF.Entidades
{
    public class CsvWriter
    {
        StreamWriter streamWriter;

        public CsvWriter(String filePath)
        {
            // Instancia el objeto que nos permite escribir en el archivo CSV
            streamWriter = new StreamWriter(filePath);
        }

        public void WriteToCsvFile(decimal num)
        {
            // Escribe un numero en el archivo CSV
            streamWriter.WriteLine(num);
        }

        public void closeStream()
        {
            // Cierra el archivo CSV
            streamWriter.Close();
        }
    }
}