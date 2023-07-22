using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace XPlaneUtilsWPF.Forms
{
    /// <summary>
    /// Логика взаимодействия для ImportAiracWindow.xaml
    /// </summary>
    public partial class ImportAiracWindow : Window
    {
        private Task<string> task;
        private int currentAnimationStep = 0;
        private string[] buttonAnimationSteps = { "-----", ">----" , "->---" , "-->--", "--->-", "---->" };

        public ImportAiracWindow()
        {
            InitializeComponent();
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            tb_currentCycle.Text = XPU.GetActualCycle();
            tb_installedCycle.Text = XPU.GetInstalledAiracCycle();
            tb_installedGNSCycle.Text = XPU.GetInstalledGNSAiracCycle();
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog f = new OpenFileDialog();
                if (f.ShowDialog() == true)
                {
                    ((Button)sender).IsEnabled = false;
                    string path = f.FileName;

                    task = Task.Run(() => XPU.InstallAllAirac(path.Clone().ToString()));

                    DispatcherTimer timer = new();
                    timer.Interval = TimeSpan.FromMilliseconds(80);
                    timer.Tick += Timer_Tick;
                    timer.Start();

                    ((Button)sender).IsEnabled = true;
                }
                UpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (task.IsCompleted)
            {
                DispatcherTimer? timer = sender as DispatcherTimer;
                timer.Stop();
                timer.Tick -= Timer_Tick;

                MessageBox.Show(task.Result);
                bt_Choose.IsEnabled = true;
                tb_Choose.Text = "Обзор...";
            }
            else
            {
                currentAnimationStep++;

                if (currentAnimationStep >= buttonAnimationSteps.Length)
                    currentAnimationStep = 0;

                tb_Choose.Text = buttonAnimationSteps[currentAnimationStep];
            }
        }
    }
}
