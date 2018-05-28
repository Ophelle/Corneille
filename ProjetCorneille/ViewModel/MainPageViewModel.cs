using ProjetCorneille.Outils;
using System;
using System.ComponentModel;
using System.Windows;

namespace ProjetCorneille.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private string selectUserPath;
        public RelayCommand CommandBouton { get; set; }
        public RelayCommand OpenButton { get; set; }

        public MainPageViewModel()
        {
            CommandBouton = new RelayCommand(bonjour);
            OpenButton = new RelayCommand(motionDetection);
        }

        private void motionDetection(object obj)
        {
            string path = General.getPathUser();
            MessageBox.Show(path);
            XMLManager.CreateXMLMovie(path, 1);
            AForgeTools.Initialisation(path);
        }

        private static void bonjour(object obj)
        {
            MessageBox.Show("Nique ta mère");
        }


      
        public string SelectUserPath
        {
            get
            {

                return this.selectUserPath;
            }
            set
            {
                this.selectUserPath = value;
                NotifyPropertyChanged("SelectUserPath");
            }
        }
    }
}
