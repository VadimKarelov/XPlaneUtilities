using System.IO;

namespace XplaneUtils
{
    /// <summary>
    /// Автоматически обновляет XPU в папке с игрой.
    /// </summary>
    static class Updater
    {
        private static int _currentVersion = 3;

        private static string _updateSourcePath = @"D:/Projects/C#/My Projects/XplaneUtils/XplaneUtils/bin/Debug/XPUv.txt";

        public static void Update()
        {
            WriteCurrentVersion();
            if (IsNeedUpdate(GetSourceVersion()))
            {
                File.Copy(_updateSourcePath, "XplaneUtils.exe", true);
            }
        }

        private static int GetSourceVersion()
        {
            int res;
            try
            {
                StreamReader strR = new StreamReader(_updateSourcePath);

                res = int.Parse(strR.ReadLine());

                strR.Close();
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        private static bool IsNeedUpdate(int sourceVersion)
        {
            return _currentVersion < sourceVersion;
        }

        private static void WriteCurrentVersion()
        {
            StreamWriter strW = new StreamWriter("XPUv.txt");
            strW.WriteLine(_currentVersion.ToString());
            strW.Close();
        }
    }
}
