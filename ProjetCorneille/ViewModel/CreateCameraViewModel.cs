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
        private bool enableButtonOK;

        public CreateCameraViewModel()
        {
            CommandButtonChoice = new RelayCommand(FunctionCommandButtonChoice);
            CommandCreate = new RelayCommand(FunctionCommandCreate);
            Session.ZonePointList = new List<System.Drawing.Point>();
            SourceImage = "";
            VideoCameraPath = "";
            CameraNameText = "Camera" + (XMLManager.idLastCamera() + 1);
            EnableButtonOK = false;
        }

        private void FunctionCommandCreate(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Etes vous sure de vouloir créer une nouvelle zone ? ", "Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Outils.XMLManager.AddCameraInXMLCameras(CameraNameText, Session.ZonePointList);
                MessageBox.Show("Création effectuée !");
            }
        }

        private void FunctionCommandButtonChoice(object obj)
        {
            VideoCameraPath = General.getPathUser();
            if (!String.IsNullOrEmpty(VideoCameraPath))
            {
                SourceImage = Outils.General.createPreviewFromVideo(VideoCameraPath, CameraNameText);
            }
            

        }

        public RelayCommand CommandButtonChoice { get; set; }
        public RelayCommand CommandButtonValider { get; set; }
        public RelayCommand CommandCreate { get; set; }
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
                EnableButtonOK = !String.IsNullOrEmpty(this.cameraNameText);

            }
        }

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

        public bool EnableButtonOK
        {
            get
            {
                return this.enableButtonOK;
            }
            set
            {
                this.enableButtonOK = value;
                NotifyPropertyChanged("EnableButtonOK");
            }
        }



    }
}
