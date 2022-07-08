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
    /// Логика взаимодействия для SceneryPackWindow.xaml
    /// </summary>
    public partial class SceneryPackWindow : Window
    {
        public SceneryPackWindow()
        {
            InitializeComponent();

            //SortSceneryPacks_Click(new object(), new RoutedEventArgs());
        }

        private async void SortSceneryPacks_ClickAsync(object sender, RoutedEventArgs e)
        {
            tb_Status.Text = "Выполняется...";

            await Task.Run(() => XPU.SortSceneryPack());

            tb_Status.Text = "Загрузка результатов...";

            listBox_ResultList.ItemsSource = XPU.ReadFile(@$"{XPU.RootPath}/Custom Scenery/scenery_packs.ini").Split("\n");

            tb_Status.Text = "Сортировка выполнена";
        }
    }
}
