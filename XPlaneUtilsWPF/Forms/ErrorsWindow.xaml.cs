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
using System.Windows.Shapes;

namespace XPlaneUtilsWPF.Forms
{
    /// <summary>
    /// Логика взаимодействия для ErrorsWindow.xaml
    /// </summary>
    public partial class ErrorsWindow : Window
    {
        public ErrorsWindow()
        {
            InitializeComponent();
        }

        private void LoadErrors_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listBox_ErrorsList.ItemsSource = XPU.GetErrors();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
