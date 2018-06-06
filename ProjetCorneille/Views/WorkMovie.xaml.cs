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
using System.Windows.Navigation;
using ProjetCorneille.ViewModel;
using ProjetCorneille.Outils;

namespace ProjetCorneille.Views
{

    /// <summary>
    /// Logique d'interaction pour WorkMovie.xaml
    /// </summary>


    public partial class WorkMovie : Window
    {

        public static bool isPlaying = true;        
        public string motionPath;
        public static int count = 0;
        public static string eventStartString;
        public static string eventEndString;

        // Acces to next Motion
        public RelayCommand NextMotion { get; set; }

        // Acces to previpous Motion
        public RelayCommand PreviousMotion { get; set; }

        public WorkMovie()
        {
            InitializeComponent();
            this.DataContext = new WorkingMovieViewModel() ;
            IsPlaying(isPlaying);
            btnPlay.IsEnabled = true;
            
        }

        public void stopVideo_Click_Motion(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
            IsPlaying(false);
            btnPlay.IsEnabled = true;
            btnPlay.Content = "Démarrer";

        }

        private void IsPlaying(bool flag)
        {
            btnPlay.IsEnabled = flag;
            btnStop.IsEnabled = flag;
            btnMoveBack.IsEnabled = flag;
            btnMoveForward.IsEnabled = flag;
          
        }

    private void btnPlay_Click(object sender, RoutedEventArgs e)
            {
            if (count == 0)
            {
                motionPath = "C:\\MotionsVideo\\" + WorkingMovieViewModel.fileName + ".avi";
                MediaPlayer.Source = new Uri(motionPath);
                btnPlay.IsEnabled = true;
                count = +1;
            }


            IsPlaying(true);
                if (btnPlay.Content.ToString() == "Démarrer")
                { 
                    MediaPlayer.Play();
                    btnPlay.Content = "Pause";
                    
            }
                else
                {
                    MediaPlayer.Pause();
                    btnPlay.Content = "Démarrer";
                }
            }

        public void btnStop_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
            btnPlay.Content = "Démarrer";
            IsPlaying(false);
            btnPlay.IsEnabled = true;
        }

        private void btnMoveBack_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Position -= TimeSpan.FromSeconds(10);
        }

        private void btnMoveForward_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Position += TimeSpan.FromSeconds(10);
        }

        private void eventStart(object sender, RoutedEventArgs e)
        {
            eventStartString = MediaPlayer.Position.ToString();
        }

        private void eventEnd(object sender, RoutedEventArgs e)
        {
            eventEndString = MediaPlayer.Position.ToString();
        }

    }
}
