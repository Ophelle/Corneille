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
using System.Windows.Threading;
using System.ComponentModel;

namespace ProjetCorneille.Views
{

    public partial class WorkMovie : Window
    {
        public string motionPath;
        public static int count = 0;
        public static string eventStartString;
        public static string eventEndString;
        DispatcherTimer _timer = new DispatcherTimer();

        
        // Permet de pouvoir allez directement à la motion suivante
        public RelayCommand NextMotion { get; set; }

        
        // Permet de pouvoir allez directement à la motion precedente
        public RelayCommand PreviousMotion { get; set; }

        public WorkMovie()
        {
            InitializeComponent();
            this.DataContext = new WorkingMovieViewModel() ;
            IsPlaying(true);
            btnPlay.IsEnabled = true;
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(ticktock);
            _timer.Start();
            TextBlock textBlock = new TextBlock();
        }

        // Permet de pouvoir afficher la durée total de la vidéo
        public string TextBlock { get; set; }

        // Permet de pouvoir alimenter les variables "la barre de temps" et "la durée total de la vidéo"
        private void ticktock(object sender, EventArgs e)
        {
            sliderSeek.Value = MediaPlayer.Position.Seconds;
            if (MediaPlayer.NaturalDuration.ToString() == "Automatic")
            {
                textBlock.Text = null;
            }
            else
            {
                textBlock.Text = MediaPlayer.NaturalDuration.ToString().Remove((MediaPlayer.NaturalDuration.ToString()).Length - 8);
                sliderSeek.Maximum = MediaPlayer.NaturalDuration.TimeSpan.Seconds;
            }
          
        }

        // Permet de pouvoir detecter la position de la barre de temps
        private void sliderSeek_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int pos = Convert.ToInt32(sliderSeek.Value);
            MediaPlayer.Position = new TimeSpan(0, 0, 0, pos, 0);
        }

        // Permet de pouvoir arreter la vidéo au moment de clicker le button "Suivant"
        public void stopVideo_Click_Motion(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
            IsPlaying(false);
            btnPlay.IsEnabled = true;
            btnPlay.Content = "Démarrer";
        }

        // Permet de pouvoir controller les booleans des buttons 
        private void IsPlaying(bool flag)
        {
            btnPlay.IsEnabled = flag;
            btnStop.IsEnabled = flag;
            btnMoveBack.IsEnabled = flag;
            btnMoveForward.IsEnabled = flag;        
        }
        // Permet de pouvoir recuperer le chemin de vidéo
        private void btnPlay_Click(object sender, RoutedEventArgs e)
            {
            if (count == 0)
            {
                motionPath = "C:\\MotionsVideo\\" + WorkingMovieViewModel.fileName + ".avi";
                MediaPlayer.Source = new Uri(motionPath);
                btnPlay.IsEnabled = true;
                
                /*
                 * Ce variable est crée pour ne pas alimenter Media Element avec le meme chemin de fichier plusieurs fois
                 */
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
        // Permet de pouvoir arreter la lecture de vidéo
        public void btnStop_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
            btnPlay.Content = "Démarrer";
            IsPlaying(false);
            btnPlay.IsEnabled = true;
            motionPath = "C:\\MotionsVideo\\" + WorkingMovieViewModel.fileName + ".avi";
            MediaPlayer.Source = new Uri(motionPath);
        }

        // Permet de pouvoir reculer dans la lecture de vidéo
        private void btnMoveBack_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Par defaut c'est 10 secondes.
             */
            MediaPlayer.Position -= TimeSpan.FromSeconds(10);
        }

        // Permet de pouvoir avancer dans la lecture de vidéo
        private void btnMoveForward_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Par defaut c'est 10 secondes.
             */
            MediaPlayer.Position += TimeSpan.FromSeconds(10);
        }

        // Permet de pouvoir recuperer debut de marqueurs
        private void eventStart(object sender, RoutedEventArgs e)
        {
            eventStartString = MediaPlayer.Position.ToString();
        }

        // Permet de pouvoir recuperer fin de marqueurs
        private void eventEnd(object sender, RoutedEventArgs e)
        {
            eventEndString = MediaPlayer.Position.ToString();
        }

    }
}
