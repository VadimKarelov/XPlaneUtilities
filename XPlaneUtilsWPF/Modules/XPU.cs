using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using XPlaneUtilsWPF.Modules;
using XPlaneUtilsWPF.Modules.LandingRate;
using XPlaneUtilsWPF.Modules.SortSceneryPack;
using static System.Net.WebRequestMethods;

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

        private static SimulatorVersionEnum SimulatorVersion
        {
            get
            {
                string ver = RootPath.Remove(0, RootPath.Length - 2);

                switch (ver)
                {
                    case "10": return SimulatorVersionEnum.XPlane10;
                    case "11": return SimulatorVersionEnum.XPlane11;
                    case "12": return SimulatorVersionEnum.XPlane12;
                    default: return SimulatorVersionEnum.XPlane10;
                }
            }
        }

        private static string TemporaryDirectory
        {
            get
            {
                string temp = Environment.CurrentDirectory + "\\temp";
                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }
                return temp;
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
            string[] errorsExample = { "Failed to find resource", "ERROR", "Terrain radar plugin: found unsupported aircraft" };

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

        #region AIRAC
        public static string InstallAllAirac(string path)
        {
            CleanTemporaryDirectory();

            string messageToReturn = "";

            messageToReturn += $"Установка GNS430: {TryInstallGNSAirac(path)}\n";

            string simVer = "";
            switch (SimulatorVersion)
            {
                case SimulatorVersionEnum.XPlane10: simVer = "XPlane10"; break;
                case SimulatorVersionEnum.XPlane11: simVer = "XPlane11"; break;
                case SimulatorVersionEnum.XPlane12: simVer = "XPlane12"; break;
            }

            messageToReturn += $"Установка дефолтного AIRAC ({simVer}): {TryInstallXPlaneAirac(path)}\n";

            return messageToReturn;
        }

        private static bool TryInstallGNSAirac(string path)
        {
            try
            {
                // Открываем архив со всеми папками
                using (RarArchive mainArchive = RarArchive.Open(path))
                {
                    ICollection<RarArchiveEntry> filesFromMainArchive = mainArchive.Entries;
                    RarArchiveEntry? requiredGNSArchiveEntry = filesFromMainArchive.FirstOrDefault(x => x.Key.Contains("xplane_customdata_native_"));

                    // Вытаскиваем файл из архива во временную папку
                    requiredGNSArchiveEntry.WriteToDirectory(TemporaryDirectory, new ExtractionOptions() { Overwrite = true });
                }

                // Получаем название архива
                string archiveWithDataPath = Directory.GetFiles(TemporaryDirectory).First();

                string installationPath = $"{RootPath}\\Custom Data\\GNS430";

                if (!Directory.Exists(installationPath))
                    Directory.CreateDirectory(installationPath);

                // Извлекаем данные в требуемую папку
                using (ZipArchive archiveWithData = ZipArchive.Open(archiveWithDataPath))
                {
                    archiveWithData.WriteToDirectory(installationPath, new ExtractionOptions() { Overwrite = true, ExtractFullPath = true });
                }

                CleanTemporaryDirectory();
                return true;
            }
            catch
            {
                CleanTemporaryDirectory();
                return false;
            }
        }

        private static bool TryInstallXPlaneAirac(string path)
        {
            try
            {
                string requiredArchiveKey = "";
                switch (XPU.SimulatorVersion)
                {
                    case SimulatorVersionEnum.XPlane10: requiredArchiveKey = "xplane10_native_"; break;
                    case SimulatorVersionEnum.XPlane11: requiredArchiveKey = "xplane11_native_"; break;
                    case SimulatorVersionEnum.XPlane12: requiredArchiveKey = "xplane12_native_"; break;
                }

                // Открываем архив со всеми папками
                using (RarArchive mainArchive = RarArchive.Open(path))
                {
                    ICollection<RarArchiveEntry> filesFromMainArchive = mainArchive.Entries;
                    RarArchiveEntry? requiredGNSArchiveEntry = filesFromMainArchive.FirstOrDefault(x => x.Key.Contains(requiredArchiveKey));

                    // Вытаскиваем файл из архива во временную папку
                    requiredGNSArchiveEntry.WriteToDirectory(TemporaryDirectory, new ExtractionOptions() { Overwrite = true });
                }

                // Получаем название архива
                string archiveWithDataPath = Directory.GetFiles(TemporaryDirectory).First();

                string installationPath = $"{RootPath}\\Custom Data";

                if (!Directory.Exists(installationPath))
                    Directory.CreateDirectory(installationPath);

                // Извлекаем данные в требуемую папку
                using (ZipArchive archiveWithData = ZipArchive.Open(archiveWithDataPath))
                {
                    archiveWithData.WriteToDirectory(installationPath, new ExtractionOptions() { Overwrite = true, ExtractFullPath = true });
                }

                CleanTemporaryDirectory();
                return true;
            }
            catch
            {
                CleanTemporaryDirectory();
                return false;
            }
        }

        private static void CleanTemporaryDirectory()
        {
            Directory.Delete(TemporaryDirectory, true);
        }

        public static string GetActualCycle()
        {
            DateTime today = DateTime.Now.ToUniversalTime();
            // Начало отсчета циклов от 2 Jan 2020 (Cycle 2001)
            DateTime currentCycleDate = new DateTime(2020, 01, 02);
            int currentCycle = 2001;

            while (currentCycleDate < today.AddDays(-28))
            {
                currentCycleDate = currentCycleDate.AddDays(28);

                if (currentCycleDate.Month == 1 && currentCycleDate.Day <= 28)
                {
                    // январь

                    // увеличиваем год
                    currentCycle += 100;
                    //убираем месяцы
                    currentCycle = (currentCycle / 100) * 100 + 1;
                }                
                else
                {
                    currentCycle += 1;
                }
            }

            return currentCycle.ToString();
        }

        //private static string Format(int n)
        //{
        //    if (n > 10)
        //        return n.ToString();
        //    else
        //        return "0" + n.ToString();
        //}

        public static string GetInstalledAiracCycle()
        {
            string path = $"{RootPath}\\Custom Data\\cycle_info.txt";

            try
            {
                string file = XPU.ReadFile(path);
                string[] lines = file.Split('\n').SelectMany(x => x.Split(':')).ToArray();
                string res = lines[1].Replace('\r', ' ');
                return res;
            }
            catch
            {
                return "Не установлено";
            }
        }

        public static string GetInstalledGNSAiracCycle()
        {
            string path = $"{RootPath}\\Custom Data\\GNS430\\navdata\\cycle_info.txt";

            try
            {
                string file = XPU.ReadFile(path);
                string[] lines = file.Split('\n').SelectMany(x => x.Split(':')).ToArray();
                string res = lines[1].Replace('\r', ' ');
                return res;
            }
            catch
            {
                return "Не установлено";
            }
        }
        #endregion
    }
}
