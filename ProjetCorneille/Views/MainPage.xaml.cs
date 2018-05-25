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
        MainPageViewModel mainPageViewModel = new MainPageViewModel();
        Boolean click;
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = mainPageViewModel;
            click = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button button = (Button)sender;
            //OpencvTools.swithOnTheCamera();
            if (click)
            {
                AForgeTools.Initialisation();
                button.Content = "Fermer";
                click = false;
            }
            else
            {
                AForgeTools.stopCamera();
                button.Content = "Open";
                click = true;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.mainPageViewModel.SelectUserPath =  General.getPathUser();
            XMLManager.CreateXMLMovie(General.getPathUser(),1);
            MessageBox.Show(this.mainPageViewModel.SelectUserPath);
            OpencvTools.openVideoFile(this.mainPageViewModel.SelectUserPath);
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a new BitmapImage.
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(@"E:\Corneille\Corneille\ProjetCorneille\Resources\Capture001.png");
            b.EndInit();

            // ... Get Image reference from sender.
            var image = sender as Image;
            // ... Assign Source.
            image.Source = b;
        }

        private void MenuClickVisu(object sender, RoutedEventArgs e)
        {
            WorkMovie wm = new WorkMovie();
           
           if ( wm.DialogResult != false)
            {
                wm.Show();
            }
        }
    }
}
