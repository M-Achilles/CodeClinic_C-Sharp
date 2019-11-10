using System;
using System.Collections.Generic;
using System.IO;

namespace Barometer
{
    public static class FileImport
    {
        public static string ImportFile(string path)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ImportFolder(string path)
        {
            string[] filesInDir = Directory.GetFiles(path, "*.txt");
            string returnString = string.Empty;

            foreach (var file in filesInDir)
            {
                string.Concat(returnString,ImportFile(file));
            }
            return returnString;
        }
    }
}
