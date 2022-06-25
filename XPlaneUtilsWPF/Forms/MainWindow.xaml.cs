using Microsoft.Win32;
using System;
using System.Windows;
using XPlaneUtilsWPF.Forms;

namespace XPlaneUtilsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetPath_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Укажите путь к X-Plane.exe", "Уведомление");

            OpenFileDialog f = new OpenFileDialog();
            if (f.ShowDialog() == true)
            {
                XPU.RootPath = f.FileName.Remove(f.FileName.LastIndexOf("\\"));
                tb_Path.Text = XPU.RootPath;
            }
        }

        private void ShowErrors_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new ErrorsWindow().ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TuneGraphics_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new GraphicsWindow().ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SortSceneryPack_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
