using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.ServiceModel;
using DissertationThemes.ServiceApp;

namespace DissertationThemes.GeneratorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            WcfClient = new ChannelFactory<IDissertationThemesService>("WordClient");
            WcfProxy = WcfClient.CreateChannel();
            ButtonGenerator.IsEnabled = false;
            ComboBoxYear.ItemsSource = WcfProxy.GetThemeYears();
            ComboBoxYear.SelectedIndex = 0;
            ComboBoxStPrograms.ItemsSource = WcfProxy.GetStudyPrograms();
            ComboBoxStPrograms.SelectedIndex = 0;
            DataGridFilteredItems.ItemsSource = WcfProxy.GetThemes(2018, 60);
        }

        public IDissertationThemesService WcfProxy { get; set; }
        public ChannelFactory<IDissertationThemesService> WcfClient { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selected = (ThemeDto)DataGridFilteredItems.SelectedItem;
            
            WcfProxy.GenerateDocx(selected.Id);

        }

        private void DataGridFilteredItems_OnSelected(object sender, RoutedEventArgs e)
        {
            ButtonGenerator.IsEnabled = true;
        }

        private void ComboBoxStPrograms_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //DataGridFilteredItems.ItemsSource = null;
            var prog = (StProgramDto) ComboBoxStPrograms.SelectedItem;
            var year = ComboBoxYear.SelectedItem;
            if (year != null && !String.IsNullOrWhiteSpace(prog.Id.ToString()) && !String.IsNullOrWhiteSpace(year.ToString()))
            {
                int st = prog.Id;
                int rok = Int32.Parse(year.ToString());
                DataGridFilteredItems.ItemsSource = WcfProxy.GetThemes(rok, st);
            }

        }

        private void ComboBoxYear_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataGridFilteredItems.ItemsSource = null;
            var prog = (StProgramDto)ComboBoxStPrograms.SelectedItem;
            var year = ComboBoxYear.SelectedItem;
            if (prog != null && !String.IsNullOrWhiteSpace(prog.Id.ToString()) && !String.IsNullOrWhiteSpace(year.ToString()))
            {
                int st = prog.Id;
                int rok = Int32.Parse(year.ToString());
                DataGridFilteredItems.ItemsSource = WcfProxy.GetThemes(rok, st);
            }

        }

        private void DataGridFilteredItems_OnLostFocus(object sender, RoutedEventArgs e)
        {
            //ButtonGenerator.IsEnabled = false;
        }
    }
}
