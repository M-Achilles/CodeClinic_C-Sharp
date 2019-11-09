using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Barometer
{
    public static class FileImport
    {
        public static StringReader ImportFile(string path)
        {
            string textFromFile = File.ReadAllText(path);

            if (path.EndsWith(".txt"))
            {
                return new StringReader(textFromFile);
            }
            else
            {
                throw new NotSupportedException("File is not of expected type. Please only use txt files");
            }
        }

        public static List<StringReader> ImportFolder(string path)
        {
            List<StringReader> srFilesInDir = new List<StringReader>();
            string[] filesInDir = Directory.GetFiles(path, "*.txt");

            foreach (var file in filesInDir)
            {
                srFilesInDir.Add(ImportFile(file));
            }
            return srFilesInDir;
        }
    }
}
