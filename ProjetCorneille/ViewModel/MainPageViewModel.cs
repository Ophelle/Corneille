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

        public MainPageViewModel()
        {
            CommandBouton = new RelayCommand(bonjour);
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
