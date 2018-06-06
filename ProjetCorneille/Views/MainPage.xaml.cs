using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ProjetCorneille.Outils;
using ProjetCorneille.ViewModel;

namespace ProjetCorneille.Views
{
    /// <summary>
    /// Logique d'interaction pour EcranPrincipal.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MainPageViewModel();
        }

    }
}
