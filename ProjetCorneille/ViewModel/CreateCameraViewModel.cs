using Accord.Video.FFMPEG;
using ProjetCorneille.Outils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            CommandButtonValider = new RelayCommand(FunctionCommandButtonValider);
            SourceImage = "";
            VideoCameraPath = "";
        }

        private void FunctionCommandButtonValider(object obj)
        {
            SourceImage = Outils.General.createPreviewFromVideo(VideoCameraPath);
        }

        private void FunctionCommandButtonChoice(object obj)
        {
            VideoCameraPath = General.getPathUser();

        }

        public RelayCommand CommandButtonChoice { get; set; }
        public RelayCommand CommandButtonValider { get; set; }
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



    }
}
