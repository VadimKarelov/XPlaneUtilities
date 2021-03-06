using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using XPlaneUtilsWPF.Modules.LandingRate;
using XPlaneUtilsWPF.Modules.SortSceneryPack;

namespace XPlaneUtilsWPF
{
    public static class XPU
    {
        public static string RootPath { get; set; }

        public static bool IsXplaneRunning
        {
            get
            {
                List<string> procList = Process.GetProcesses().Select(p => p.ProcessName).ToList();
                return procList.IndexOf("X-Plane") != -1;
            }
        }

        #region Получение списка ошибок
        public static List<string> GetErrors()
        {
            if (!IsXplaneRunning)
                return GetErrorsLines(ReadFile($"{RootPath}/Log.txt"));
            else
                throw new Exception("X-Plane должен быть закрыт!");
        }

        private static List<string> GetErrorsLines(string file)
        {
            string[] errorsExample = { "Failed to find resource" , "ERROR", "Terrain radar plugin: found unsupported aircraft" };

            string[] lines = file.Split('\n');
            List<string> errors = new List<string>();

            foreach (string line in lines)
            {
                foreach (string errorEx in errorsExample)
                {
                    if (line.Contains(errorEx))
                    {
                        string t = line.Replace("\r", "").Replace("\n", "");
                        if (errors.IndexOf(t) == -1)
                        {
                            errors.Add(t);
                        }
                    }
                }
            }

            return errors;
        }
        #endregion

        #region Улучшение теней
        public static void ShadowEnhancement(int quality)
        {
            if (!IsXplaneRunning)
            {
                string path = @$"{RootPath}/Resources/settings.txt";
                CreateBackupFile(path);
                ChangeShadowResolution(path, quality);
            }
            else
                throw new Exception("X-Plane должен быть закрыт!");
        }        

        private static void ChangeShadowResolution(string path, int quality)
        {
            string text = ReadFile(path);

            text = text.Replace("fbo/shadow_cam_size	256", $"fbo/shadow_cam_size	{quality}");
            text = text.Replace("fbo/shadow_cam_size	512", $"fbo/shadow_cam_size	{quality}");
            text = text.Replace("fbo/shadow_cam_size	1024", $"fbo/shadow_cam_size	{quality}");
            text = text.Replace("fbo/shadow_cam_size	2048", $"fbo/shadow_cam_size	{quality}");
            text = text.Replace("fbo/shadow_cam_size	4096", $"fbo/shadow_cam_size	{quality}");
            text = text.Replace("fbo/shadow_cam_size	8192", $"fbo/shadow_cam_size	{quality}");

            StreamWriter strW = new StreamWriter(path);
            strW.Write(text);
            strW.Close();
        }
        #endregion

        #region Сортировка scenery_pack.ini
        public static void SortSceneryPack()
        {
            if (!IsXplaneRunning)
            {
                string path = @$"{RootPath}/Custom Scenery/scenery_packs.ini";
                CreateBackupFile(path);
                string sortedFile = ScenerySorter.SortFile(ReadFile(path));

                StreamWriter strW = new StreamWriter(path, false);
                strW.Write(sortedFile);
                strW.Close();
            }
            else
                throw new Exception("X-Plane должен быть закрыт!");
        }
        #endregion

        #region LandingRate
        public static List<LandingRateRecord> GetLandingRateLog()
        {
            if (!IsXplaneRunning)
                return LandingRateHandler.GetLog(ReadFile($"{RootPath}/LandingRate.log"));
            else
                throw new Exception("X-Plane должен быть закрыт!");
        }
        #endregion

        #region Работа с файлами
        public static string ReadFile(string path)
        {
            try
            {
                StreamReader strR = new StreamReader(path, Encoding.Default);

                string text = strR.ReadToEnd();

                strR.Close();

                return text;
            }
            catch
            {
                throw new Exception("Не правильно выбран путь");
            }
        }

        private static void CreateBackupFile(string path)
        {
            string text = ReadFile(path);

            StreamWriter strW = new StreamWriter($"{path}.bak{DateTime.Now.ToShortDateString()}{DateTime.Now.ToLongTimeString().Replace(":", "")}");
            strW.Write(text);
            strW.Close();
        }
        #endregion
    }
}
