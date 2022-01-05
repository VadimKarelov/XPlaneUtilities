using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XplaneUtils
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ShowErrorsList(object sender, EventArgs e)
        {
            button_ErrorsList.Enabled = false;
            label_Warning.Text = "Поиск ошибок инициирован";
            try
            {
                StreamReader strR = new StreamReader("Log.txt", Encoding.Default);
                List<string> errors = GetErrorsLines(strR.ReadToEnd());
                strR.Close();
                label_Warning.Text = "Файл считан";
                ShowErrors(errors);
                label_Warning.Text = "Успешно";
            }
            catch
            {
                label_Warning.Text = "Убедитесь, что программа лежит в папке с игрой";
            }
            button_ErrorsList.Enabled = true;
        }

        private List<string> GetErrorsLines(string file)
        {
            string[] errorsExample = { "Failed to find resource" };

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

        private void ShowErrors(List<string> errors)
        {
            string msg = "";
            foreach (string error in errors)
            {
                msg += error + "\n";
            }
            MessageBox.Show(msg);
        }
    }
}
