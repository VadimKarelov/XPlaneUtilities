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
using XPlaneUtilsWPF.Modules.LandingRate;

namespace XPlaneUtilsWPF.Forms
{
    /// <summary>
    /// Логика взаимодействия для LandingRateWindow.xaml
    /// </summary>
    public partial class LandingRateWindow : Window
    {
        private List<LandingRateRecord> _records;
        private List<Aircraft> _aircrafts;

        public LandingRateWindow()
        {
            InitializeComponent();
            Load_ClickAsync(new object(), new RoutedEventArgs());
        }

        private async void Load_ClickAsync(object sender, RoutedEventArgs e)
        {
            bt_Load.IsEnabled = false;

            _records = await Task.Run(() => XPU.GetLandingRateLog());

            _records.Sort();

            dg_landingRate.ItemsSource = _records;
            
            lb_aircrafts.ItemsSource = GetAircraftList();

            bt_Load.IsEnabled = true;
        }

        private List<string> GetAircraftList()
        {
            List<string> aircrafts = _records.Select(x => x.Aircraft).Distinct().ToList();
            // add average descending speed and average ovreload to each aircraft
                        
            //aircrafts = aircrafts.Select(x => $"{x} (DS:{Math.Round(_records.Where(y => y.Aircraft == x).Average(z => z.DescendingSpeed), 3)} O:{Math.Round(_records.Where(y => y.Aircraft == x).Average(z => z.Overload), 2)})").ToList();
            
            aircrafts.Add("");
            aircrafts.Sort();
            _aircrafts = aircrafts.Select(x => new Aircraft(x, MathF.Round(_records.Where(y => y.Aircraft.Contains(x)).Average(z => z.DescendingSpeed), 3), MathF.Round(_records.Where(y => y.Aircraft.Contains(x)).Average(z => z.Overload), 2))).ToList();

            aircrafts = _aircrafts.Select(x => $"{x.Name} (DR:{x.AverageDescendingRate} O:{x.AverageOverloadRate})").ToList();

            return aircrafts;
        }

        private void Aircraft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_aircrafts.SelectedItem != null)
            {
                dg_landingRate.ItemsSource = _records.Where(x => x.Aircraft.Contains(_aircrafts[lb_aircrafts.SelectedIndex].Name));
            }
        }

        class Aircraft
        {
            public string Name { get; set; }
            public float AverageDescendingRate { get; set; }
            public float AverageOverloadRate { get; set; }

            public Aircraft(string name, float averageDesRate, float averageOverload)
            {
                Name = name;
                AverageDescendingRate = averageDesRate;
                AverageOverloadRate = averageOverload;
            }
        }
    }
}
