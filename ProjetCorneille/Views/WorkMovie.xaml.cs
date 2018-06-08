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
    /// <summary>
    /// Logique d'interaction pour WorkMovie.xaml
    /// </summary>
    /// 
    public partial class WorkMovie : Window
    {
 

        public static bool isPlaying = true;        
        public string motionPath;
        public static int count = 0;
        public static string eventStartString;
        public static string eventEndString;
        private string totalDurationTime;
        DispatcherTimer _timer = new DispatcherTimer(); 

        // Acces to next Motion
        public RelayCommand NextMotion { get; set; }

        // Acces to previpous Motion
        public RelayCommand PreviousMotion { get; set; }
        public string TotalDurationTime { get => totalDurationTime; set => totalDurationTime = value; }

        public WorkMovie()
        {
            InitializeComponent();
            this.DataContext = new WorkingMovieViewModel() ;
            IsPlaying(isPlaying);
            btnPlay.IsEnabled = true;
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(ticktock);
            _timer.Start();

        }


        private void ticktock(object sender, EventArgs e)
        {
            sliderSeek.Value = MediaPlayer.Position.Seconds;
            totalDurationTime = MediaPlayer.NaturalDuration.ToString();
            //sliderSeek.Name = MediaPlayer.NaturalDuration.ToString();
        }

        private void sliderSeek_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int pos = Convert.ToInt32(sliderSeek.Value);
            MediaPlayer.Position = new TimeSpan(0, 0, 0, pos, 0);
           


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
            motionPath = "C:\\MotionsVideo\\" + WorkingMovieViewModel.fileName + ".avi";
            MediaPlayer.Source = new Uri(motionPath);
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
