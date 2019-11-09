using System;
using System.Collections.Generic;
using System.IO;

namespace Barometer
{
    public static class FileImport
    {
        public static string ImportFile(string path)
        {
            string textFromFile = File.ReadAllText(path);

            if (path.EndsWith(".txt"))
            {
                StringReader sr = new StringReader(textFromFile);
                using (sr)
                {
                    return sr.ReadToEnd();
                }
            }
            else
            {
                throw new NotSupportedException("File is not of expected type. Please only use txt files");
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
