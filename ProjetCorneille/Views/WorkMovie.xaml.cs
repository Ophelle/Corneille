﻿using System;
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

namespace ProjetCorneille.Views
{

    /// <summary>
    /// Logique d'interaction pour WorkMovie.xaml
    /// </summary>
    public partial class WorkMovie : Window
    {

        public string motionPath;

        public WorkMovie()
        {
            InitializeComponent();
            this.DataContext = new WorkingMovieViewModel() ;
            IsPlaying(false);

           
        }

        

        private void IsPlaying(bool flag)
        {
            btnPlay.IsEnabled = true;
            btnStop.IsEnabled = flag;
            btnMoveBack.IsEnabled = flag;
            btnMoveForward.IsEnabled = flag;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            IsPlaying(true);
            if (btnPlay.Content.ToString() == "Play")
            {
                // TODO FIX PATH PROBLEM
                motionPath = "C:\\MotionsVideo\\" + WorkingMovieViewModel.nameOfMotionPath + ".avi";
                MediaPlayer.Source = new Uri(motionPath);
               // MediaPlayer.Source = new Uri("C:\\MotionsVideo\\20180529_125628_203_motion1_motion1.avi");

                MediaPlayer.Play();
                btnPlay.Content = "Pause";
            }
            else
            {
                MediaPlayer.Pause();
                btnPlay.Content = "Play";
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
            btnPlay.Content = "Play";
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

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
           
               
                btnPlay.IsEnabled = true;
  
        }
    }
}
