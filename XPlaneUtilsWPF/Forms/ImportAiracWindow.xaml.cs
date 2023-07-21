using System.Windows;

namespace XPlaneUtilsWPF.Forms
{
    /// <summary>
    /// Логика взаимодействия для ImportAiracWindow.xaml
    /// </summary>
    public partial class ImportAiracWindow : Window
    {
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
    }
}
