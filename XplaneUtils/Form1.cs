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
        Task showErrorsTask;

        public Form1()
        {
            InitializeComponent();
        }

        private void CheckThreads_Tick(object sender, EventArgs e)
        {
            if (showErrorsTask != null && showErrorsTask.IsCompleted)
            {
                button_ErrorsList.Text = "Список ошибок";
                button_ErrorsList.Enabled = true;
            }
        }

        private void ShowErrorsList_Clicked(object sender, EventArgs e)
        {
            button_ErrorsList.Enabled = false;
            button_ErrorsList.Text = "Поиск ошибок инициирован";
            showErrorsTask = new Task(() => ShowErrorsList());
            showErrorsTask.Start();
        }

        private void ShowErrorsList()
        {            
            try
            {
                ShowErrors(XPU.GetErrors());
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Пожалуйста, убедитесь, что программа лежит в папке с игрой!");
            }
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
