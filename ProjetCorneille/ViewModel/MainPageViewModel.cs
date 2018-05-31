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
        public RelayCommand CommandOpenButton { get; set; }

        public MainPageViewModel()
        {
            CommandMenuVisu = new RelayCommand(FunctionCommandMenuVisu);
            CommandMenuCreateCamera = new RelayCommand(FunctionCommandMenuCreateCamera);
            CommandOpenButton = new RelayCommand(motionDetection);
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
