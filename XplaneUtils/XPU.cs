using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XplaneUtils
{
    public static class XPU
    {
        public static List<string> GetErrors()
        {
            return GetErrorsLines(ReadLogFile());
        }

        private static string ReadLogFile()
        {
            StreamReader strR = new StreamReader("Log.txt", Encoding.Default);

            string text = strR.ReadToEnd();

            strR.Close();

            return text;
        }

        private static List<string> GetErrorsLines(string file)
        {
            string[] errorsExample = { "Failed to find resource" , "ERROR" };

            string[] lines = file.Split('\n');
            List<string> errors = new List<string>();

            foreach (string line in lines)
            {
                foreach (string errorEx in errorsExample)
                {
                    if (line.Contains(errorEx))
                    {
                        errors.Add(line);
                    }
                }
            }

            return errors;
        }
    }
}
