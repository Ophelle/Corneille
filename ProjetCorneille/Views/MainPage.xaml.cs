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
using ProjetCorneille.Outils;
using ProjetCorneille.ViewModel;

namespace ProjetCorneille.Views
{
    /// <summary>
    /// Logique d'interaction pour EcranPrincipal.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MainPageViewModel mainPageViewModel = new MainPageViewModel();
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = mainPageViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpencvTools.swithOnTheCamera();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.mainPageViewModel.SelectUserPath =  General.getPathUser();
            XMLManager.CreateXMLMovie(General.getPathUser(),1);
            MessageBox.Show(this.mainPageViewModel.SelectUserPath);
            OpencvTools.openVideoFile(this.mainPageViewModel.SelectUserPath);
        }
    }
}
