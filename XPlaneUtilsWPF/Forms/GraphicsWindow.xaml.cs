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
    /// Логика взаимодействия для GraphicsWindow.xaml
    /// </summary>
    public partial class GraphicsWindow : Window
    {
        public GraphicsWindow()
        {
            InitializeComponent();
        }

        private void ChangeShadowResolution_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Будет изменен файл, который Laminar Research настоятельно не рекомендует изменять. " +
                    "В случае ошибок симулятора восстановите старый файл из файла settings.txt.bak.", "Предупреждение",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    XPU.ShadowEnhancement((int)sl_ShadowRes.Value);
                    MessageBox.Show("Создана копия файла settings.txt (Resources/settings.txt.bak)");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }
}
