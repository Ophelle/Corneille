using ProjetCorneille.Outils;
using ProjetCorneille.Views;
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
            CommandMenuVisu = new RelayCommand(FunctionCommandMenuVisu);
            CommandMenuCreateCamera = new RelayCommand(FunctionCommandMenuCreateCamera);
            OpenButton = new RelayCommand(motionDetection);
        }

        private void motionDetection(object obj)
        {
            string path = General.getPathUser();
            MessageBox.Show(path);
            XMLManager.CreateXMLMovie(path, 1);
            AForgeTools.Initialisation(path);
        }

        private void FunctionCommandMenuCreateCamera(object obj)
        {
           CreateCameraWindow ccw = new CreateCameraWindow();
           ccw.Show();
        }

        private static void bonjour(object obj)
        {
            MessageBox.Show("Nique ta mère");
        }

        public RelayCommand CommandMenuVisu { get; set; }
        public RelayCommand CommandMenuCreateCamera { get; }

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

        private void FunctionCommandMenuVisu (object obj)
        {
            WorkMovie wm = new WorkMovie();

            if (wm.DialogResult != false)
            {
                wm.Show();
            }
        }
    }
}
