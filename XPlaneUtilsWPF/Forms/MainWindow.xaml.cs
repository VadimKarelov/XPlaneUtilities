using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            new ErrorsWindow().ShowDialog();
        }
    }
}
