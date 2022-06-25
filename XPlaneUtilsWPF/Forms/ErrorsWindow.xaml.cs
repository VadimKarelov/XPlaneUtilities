using System.Windows;

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
            LoadErrors_Click(new object(), new RoutedEventArgs());
        }

        private void LoadErrors_Click(object sender, RoutedEventArgs e)
        {
            listBox_ErrorsList.ItemsSource = XPU.GetErrors();
        }
    }
}
