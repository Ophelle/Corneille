using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjetCorneille.Outils;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjetCorneille.ViewModel
{
    class WorkingMovieViewModel : ViewModelBase
    {
        private string nameOfVideo;
        private string nameOfMotion;
        private DateTime date;
        private bool startAndStop;
        private string category;
        private string comment;
        private bool buttonValVol0;
        private bool buttonValVol1;
        private bool buttonValVol2;

        //Video lecture buttons
        private bool btnPlay;
        private bool btnStop;
        private bool btnMoveBack;
        private bool btnMoveForward;
        private bool btnOpen;

        private string btnPlayContent;
        private string btnStopContent;
        private string btnMoveBackContent;
        private string btnMoveForwardContent;
        private string btnOpenContent;

        public bool BtnPlay
        {
            get
            {
                return this.btnPlay;
            }
            set
            {
                this.btnPlay = value;
                NotifyPropertyChanged("BtnPlay");
            }
        }

        public string BtnPlayContent
        {
            get
            {
                return this.btnPlayContent;
            }
            set
            {
                this.btnPlayContent = value;
                NotifyPropertyChanged("BtnPlayContent");
            }

        }

        public bool BtnStop
        {
            get
            {
                return this.btnStop;
            }
            set
            {
                this.btnStop = value;
                NotifyPropertyChanged("BtnStop");
            }
        }

        public string BtnStopContent
        {
            get
            {
                return this.btnStopContent;
            }
            set
            {
                this.btnStopContent = value;
                NotifyPropertyChanged("BtnPlayContent");
            }

        }

        public bool BtnMoveBack
        {
            get
            {
                return this.btnMoveBack;
            }
            set
            {
                this.btnMoveBack = value;
                NotifyPropertyChanged("BtnMoveBack");
            }
        }

        public string BtnMoveBackContent
        {
            get
            {
                return this.btnMoveBackContent;
            }
            set
            {
                this.btnMoveBackContent = value;
                NotifyPropertyChanged("BtnMoveBackContent");
            }

        }

        public bool BtnMoveForward
        {
            get
            {
                return this.btnMoveBack;
            }
            set
            {
                this.btnMoveBack = value;
                NotifyPropertyChanged("BtnMoveBack");
            }
        }

        public string BtnMoveForwardContent
        {
            get
            {
                return this.btnMoveForwardContent;
            }
            set
            {
                this.btnMoveForwardContent = value;
                NotifyPropertyChanged("BtnMoveForwardContent");
            }

        }

        public bool BtnOpen
        {
            get
            {
                return this.btnOpen;
            }
            set
            {
                this.btnOpen = value;
                NotifyPropertyChanged("BtnOpen");
            }
        }

        public string BtnOpenContent
        {
            get
            {
                return this.btnOpenContent;
            }
            set
            {
                this.btnOpenContent = value;
                NotifyPropertyChanged("BtnOpenContent");
            }

        }

        public WorkingMovieViewModel()
        {
            /*
             TODO : Define Command for video button and bind them
             */



            //For marquer events 
            CommandButtonStart = new RelayCommand(FunctionboolToChange);
            CommandButtonStop = new RelayCommand(FunctionboolToChangeToFalse);
            CommandSaveMarqueur = new RelayCommand(FunctionboolToChange);
            ButtonValVol0 = true;
            StartAndStop = false;
            BtnPlayContent = "Play";

            //For the video lecture it has to be false before 
            IsPlaying(false);

        }

        // Button to save marqueur 
        public RelayCommand CommandSaveMarqueur { get; set; }

        // Button to start marqueur
        public RelayCommand CommandButtonStart { get; set; }

        // Bouton to stop marqueur
        public RelayCommand CommandButtonStop { get; set; }

        private void IsPlaying(bool flag)
        {
            BtnPlay = flag;
            BtnStop = flag;
            BtnMoveBack = flag;
            BtnMoveForward = flag;
        }

        private void FunctionBtnPlay(Object obj)
        {
            //we have to set true "IsEnabled", so video will start directly 
            IsPlaying(true);
            if (BtnPlayContent == "Play")
            {
                MediaPlayer.Play();
                BtnPlayContent = "Pause";
            }
            else
            {
                MediaPlayer.Pause();
                BtnPlayContent = "Play";
            }
        }

        private void FunctionBtnStop(Object obj)
        {
            MediaPlayer.Pause();
            btnPlayContent = "Play";
            IsPlaying(false);
            BtnPlay = true;
        }

        private void FunctionBtnMoveBack(Object obj)
        {
            MediaPlayer.Position -= TimeSpan.FromSeconds(10);
        }

        private void FunctionBtnMoveForward(Object obj)
        {
            //to fetch video time by TimeSpan.FromSeconds() function 
            MediaPlayer.Position += TimeSpan.FromSeconds(10);
        }

        //Open option will be for testing videos
        private void FunctionBtnOpen(Object obj)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Videos"; // Default file name
            dialog.DefaultExt = ".WMV"; // Default file extension
            dialog.Filter = "WMV file (.wm)|*.mp4"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dialog.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                MediaPlayer.Source = new Uri(dialog.FileName);
                BtnPlay = true;
            }
        }

        //


        // Methode to change le statut 
        public void FunctionboolToChange(Object obj)
        {
            StartAndStop = true;  
        }

        // Methode ta save marqueur
        public void SaveMarqueurToXml(Object obj)
        {
            if (!StartAndStop)
            {
                FunctionStartAndStopRecToXmlSaveMarqueur(this.nameOfVideo, this.nameOfMotion, this.category, this.comment, this.date);
            }
           
        }


        // Methode to change le statut 
        public void FunctionboolToChangeToFalse(Object obj)
        {
            StartAndStop = false;
        }

        public bool StartAndStop
        {
            get
            {
                return this.startAndStop;
            }
            set
            {
                this.startAndStop = value;
                NotifyPropertyChanged("StartAndStop");
            }
        }
        // Bouton de catagorie de vol 
        public bool ButtonValVol0
        {
            get
            {
                return this.buttonValVol0;
            }
            set
            {
                this.buttonValVol0 = value;
                NotifyPropertyChanged("ButtonValVol0");
                this.category = "Vol";
            }
        }
        // vol1 
        public bool ButtonValVol1
        {
            get
            {
                return this.buttonValVol1;
            }
            set
            {
                this.buttonValVol1 = value;
                NotifyPropertyChanged("ButtonValVol1");
                this.category = "Vol1";
            }
        }

        // vol2
        public bool ButtonValVol2
        {
            get
            {
                return this.buttonValVol2;
            }
            set
            {
                this.buttonValVol2 = value;
                NotifyPropertyChanged("ButtonValVol2");
                this.category = "Vol2";
            }
        }

        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
                NotifyPropertyChanged("Category");
            }
        }
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
                NotifyPropertyChanged("Comment");
            }
        }

        /**
        * @bool paramètre permettant de savoir si on doit enregistrer ou pas la video 
        * @string category type de vol constaté 
        * @comment commentaire par le marqueur 
        * @string nom de la video 
        * @string nom de la motion
        * @DateTime heure de capture  
        *
        */
        public void FunctionStartAndStopRecToXmlSaveMarqueur(string nameOfVideo, string nameOfMotion, string category, string comment, DateTime date)
        {
            if(StartAndStop)
            {
                //Insestion dans les varaibles globales
                this.nameOfMotion = nameOfMotion;
                this.nameOfVideo = nameOfVideo;
                this.category = category;
                this.comment = comment;
                this.date = date;
            }
            // Insertion dans le XMl et fin du marqueur
            else
            {
                this.comment = comment;
                this.category = category;
                XMLManager.addToXmlMarqueurMotionInMovie(this.category, this.comment, this.nameOfMotion, this.nameOfVideo, this.date, date);
            }
        }            
    }
}
