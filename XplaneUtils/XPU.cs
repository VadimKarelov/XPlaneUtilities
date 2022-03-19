﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XplaneUtils
{
    public static class XPU
    {
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
                return GetErrorsLines(ReadFile("Log.txt"));
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
                        errors.Add(line);
                    }
                }
            }

            return errors;
        }
        #endregion

        #region Улучшение теней
        public static void ShadowEnhancement()
        {
            if (!IsXplaneRunning)
            {
                string path = @"Resources/settings.txt";
                CreateBackupFile(path);
                ChangeShadowResolution(path);
            }
            else
                throw new Exception("X-Plane должен быть закрыт!");
        }

        private static void CreateBackupFile(string path)
        {
            string text = ReadFile(path);

            StreamWriter strW = new StreamWriter(path + ".bak");
            strW.Write(text);
            strW.Close();
        }

        private static void ChangeShadowResolution(string path)
        {
            string text = ReadFile(path);

            text = text.Replace("fbo/shadow_cam_size	256", "fbo/shadow_cam_size	8192");
            text = text.Replace("fbo/shadow_cam_size	512", "fbo/shadow_cam_size	8192");
            text = text.Replace("fbo/shadow_cam_size	1024", "fbo/shadow_cam_size	8192");
            text = text.Replace("fbo/shadow_cam_size	2048", "fbo/shadow_cam_size	8192");
            text = text.Replace("fbo/shadow_cam_size	4096", "fbo/shadow_cam_size	8192");

            StreamWriter strW = new StreamWriter(path);
            strW.Write(text);
            strW.Close();
        }
        #endregion

        private static string ReadFile(string path)
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
                throw new Exception("Убедитесь, что программа лежит в папке с игрой.");
            }
        }
    }
}
