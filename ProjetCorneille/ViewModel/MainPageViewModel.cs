using ProjetCorneille.Outils;
using ProjetCorneille.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows;


namespace ProjetCorneille.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private string selectUserPath;
        public RelayCommand CommandButtonChoice { get; set; }
        public RelayCommand CommandRunAnalyse { get; set; }
        public RelayCommand ButtonDisableButton { get; set; }

        private string videoPath;

        public ObservableCollection<Item> itemList;
        
        private Item valueSelectedCamera;

        public static string nameOfCamera;

        public static int idOfCamera;

        public MainPageViewModel()
        {
            CommandMenuVisu = new RelayCommand(FunctionCommandMenuVisu);
            CommandMenuCreateCamera = new RelayCommand(FunctionCommandMenuCreateCamera);
            CommandButtonChoice = new RelayCommand(ChoiceVideo);
            CommandRunAnalyse = new RelayCommand(Analyse);
            VideoPath = "";
            ItemList = XMLManager.bringCameraFromXml();
        }

        private void ChoiceVideo(object obj)
        {
            VideoPath = General.getPathUser();
        }

        // liste d item pour afficher les cameras
        public ObservableCollection<Item> ItemList
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
                NotifyPropertyChanged("ItemList");
            }
        }


        public string VideoPath
        {
            get
            {
                return this.videoPath;
            }
            set
            {
                this.videoPath = value;
                NotifyPropertyChanged("VideoPath");
            }
        }

        public Item ValueSelectedCamera
        {
            get
            {
                return this.valueSelectedCamera;
            }
            set
            {
                this.valueSelectedCamera = value;
                NotifyPropertyChanged("ValueSelectedCamera");

            }
        }

        private void Analyse(object obj)
        {
            string path = VideoPath;
            if (path != "")
            {
                if(ValueSelectedCamera.itemID > 0)
                {
                    MessageBox.Show("Veuillez appuyer sur OK pour commencer le traitement, un message sera present à la fin de celui-ci");
                    XMLManager.CreateXMLMovie(path, ValueSelectedCamera.itemID);
                    List<System.Drawing.Point> polygon = XMLManager.ReadPolygonInXMLCamera(ValueSelectedCamera.itemID);
                    AForgeTools.Initialisation(path, polygon);
                    MessageBox.Show("Traitement terminé");
                }
                else
                {
                    MessageBox.Show("Veuillez choisir une camera");
                }
            }
            else
            {
                MessageBox.Show("Veuillez choisir une video");
            }
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
