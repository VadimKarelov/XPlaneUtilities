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
        Task shadowEnhancementTask;

        public Form1()
        {
            InitializeComponent();
        }

        private void CheckThreads_Tick(object sender, EventArgs e)
        {
            if (showErrorsTask != null && showErrorsTask.IsCompleted && !button_ErrorsList.Enabled)
            {
                button_ErrorsList.Text = "Список ошибок";
                button_ErrorsList.Enabled = true;
            }
            if (shadowEnhancementTask != null && shadowEnhancementTask.IsCompleted && !button_ShadowEnhancement.Enabled)
            {
                button_ShadowEnhancement.Text = "Улучшить тени";
                button_ShadowEnhancement.Enabled = true;
            }
        }

        private void ShowErrorsList_Clicked(object sender, EventArgs e)
        {
            button_ErrorsList.Enabled = false;
            button_ErrorsList.Text = "Поиск ошибок инициирован";
            showErrorsTask = new Task(() => ShowErrorsList());
            showErrorsTask.Start();
        }

        private void ShadowEnhancement_Clicked(object sender, EventArgs e)
        {
            button_ShadowEnhancement.Enabled = false;
            button_ShadowEnhancement.Text = "Настройка теней...";
            shadowEnhancementTask = new Task(() => ShadowEnhancement());
            shadowEnhancementTask.Start();
        }

        private void ShowErrorsList()
        {            
            try
            {
                ShowErrors(XPU.GetErrors());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void ShadowEnhancement()
        {
            try
            {
                if (MessageBox.Show("Будет изменен файл, который Laminar Research настоятельно не рекомендует изменять. " +
                    "В случае ошибок симулятора восстановите старый файл из файла settings.txt.bak.", "Предупреждение",
                    MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    XPU.ShadowEnhancement();
                    MessageBox.Show("Создана копия файла settings.txt (Resources/settings.txt.bak)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   
    }
}
