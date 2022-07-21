using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using XPlaneUtilsWPF.Forms;

namespace XPlaneUtilsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _pathToPath = "path.txt";

        public MainWindow()
        {
            InitializeComponent();
            SetPath(LoadDefaultPath());
        }

        private string LoadDefaultPath()
        {
            try
            {
                using (StreamReader strR = new StreamReader(_pathToPath, false))
                {
                    return strR.ReadToEnd();
                }
            }
            catch
            {
                return @"D:\";
            }
        }

        private void SaveDefaultPath()
        {
            using (StreamWriter strW = new StreamWriter(_pathToPath, false))
            {
                strW.WriteAsync(XPU.RootPath);
                strW.Close();
            }
        }

        private void SetPath_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Укажите путь к X-Plane.exe", "Уведомление");

            OpenFileDialog f = new OpenFileDialog();
            if (f.ShowDialog() == true)
            {
                SetPath(f.FileName.Remove(f.FileName.LastIndexOf("\\")));                
            }
        }

        private void SetPath(string path)
        {
            XPU.RootPath = path;
            tb_Path.Text = XPU.RootPath;
            SaveDefaultPath();
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
            try
            {
                new SceneryPackWindow().ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LandingRate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new LandingRateWindow().ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
