using Accord.Video.FFMPEG;
using ProjetCorneille.Model;
using ProjetCorneille.Outils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace ProjetCorneille.ViewModel
{
    public class CreateCameraViewModel : ViewModelBase
    {

        private string videoCameraPath;
        private string cameraNameText;
        private string sourceImage;

        public CreateCameraViewModel()
        {
            CommandButtonChoice = new RelayCommand(FunctionCommandButtonChoice);
            CommandCreate = new RelayCommand(FunctionCommandCreate);
            //Variable static dite globale, pour que le contenu sois accessible partout
            Session.ZonePointList = new List<System.Drawing.Point>();
            SourceImage = "";
            VideoCameraPath = "";
            //Nom par défaut de la caméra
            CameraNameText = "Camera" + (XMLManager.idLastCamera() + 1);

        }

        //Méthode permettant d'enregistre la zone
        private void FunctionCommandCreate(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Etes vous sure de vouloir créer une nouvelle zone ? ", "Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                //On vérifie qu'une zone à bien été créée par l'utilisateur
                if (Session.ZonePointList.Count > 1)
                {
                    Outils.XMLManager.AddCameraInXMLCameras(CameraNameText, Session.ZonePointList);
                    MessageBox.Show("Création effectuée !");
                }
                else
                {
                    MessageBox.Show("Veuillez séléctionner une zone pour créer une caméra !");
                }

            }
        }

        //Fonction permettant de faire appel à la méthode qui à une interraction avec l'utilisateur
        //pour choisir le fichier à ouvir
        private void FunctionCommandButtonChoice(object obj)
        {
            //Appel de la méthode pour récupérer un fichier
            VideoCameraPath = General.getPathUser();
            if (!String.IsNullOrEmpty(VideoCameraPath))
            {
                SourceImage = Outils.General.createPreviewFromVideo(VideoCameraPath, CameraNameText);
            }


        }

        #region Propriétés du view model

        #region Déclaration des commandes
        public RelayCommand CommandButtonChoice { get; set; }
        public RelayCommand CommandButtonValider { get; set; }
        public RelayCommand CommandCreate { get; set; }
        #endregion  Déclaration des commandes

        //Propriété servant au chemin de la vidéo correspondant à la caméra
        public string VideoCameraPath
        {
            get
            {
                return this.videoCameraPath;
            }
            set
            {
                this.videoCameraPath = value;
                NotifyPropertyChanged("VideoCameraPath");
            }
        }

        //Propriété servant au nom de la caméra
        public string CameraNameText
        {
            get
            {
                return this.cameraNameText;
            }
            set
            {
                this.cameraNameText = value;
                NotifyPropertyChanged("CameraNameText");


            }
        }
        //Propriété servant au chemin de l'image
        public string SourceImage
        {
            get
            {
                return this.sourceImage;
            }
            set
            {
                this.sourceImage = value;
                NotifyPropertyChanged("SourceImage");
            }
        }

        #endregion Propriétés du view model
    }
}
