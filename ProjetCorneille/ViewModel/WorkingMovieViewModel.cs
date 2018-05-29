using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjetCorneille.Outils;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

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
        public MediaPlayer mediaPlayer = new MediaPlayer();

        private string btnPlayContent;
        private string btnStopContent;
        private string btnMoveBackContent;
        private string btnMoveForwardContent;
        private string btnOpenContent;

       

        public WorkingMovieViewModel()
        {
           
            //For marquer events 
            CommandButtonStart = new RelayCommand(FunctionboolToChange);
            CommandButtonStop = new RelayCommand(FunctionboolToChangeToFalse);
            CommandSaveMarqueur = new RelayCommand(FunctionboolToChange);

            //Video Lecture
            CommandBtnPlay = new RelayCommand(FunctionBtnPlay);
            CommandBtnStop = new RelayCommand(FunctionBtnStop);
            CommandBtnMoveBack = new RelayCommand(FunctionBtnMoveBack);
            CommandBtnMoveForward = new RelayCommand(FunctionBtnMoveForward);
            CommandBtnOpen = new RelayCommand(FunctionBtnOpen);

            ButtonValVol0 = true;
            StartAndStop = false;
            BtnPlayContent = "Play";
            BtnStopContent = "Stop";
            BtnMoveBackContent = "Back";
            BtnMoveForwardContent = "Forward";
            BtnOpenContent = "Open";

            //For the video lecture it has to be false before 
            IsPlaying(false);

        }

        private void IsPlaying(bool flag)
        {
            BtnPlay = flag;
            BtnStop = flag;
            BtnMoveBack = flag;
            BtnMoveForward = flag;
            BtnOpen = true;
        }

        public RelayCommand CommandBtnPlay { get; set; }
        public RelayCommand CommandBtnStop { get; set; }
        public RelayCommand CommandBtnMoveBack { get; set; }

        public RelayCommand CommandBtnMoveForward { get; set; }
        public RelayCommand CommandBtnOpen { get; set; }



        // Button to save marqueur 
        public RelayCommand CommandSaveMarqueur { get; set; }

        // Button to start marqueur
        public RelayCommand CommandButtonStart { get; set; }

        // Bouton to stop marqueur
        public RelayCommand CommandButtonStop { get; set; }

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
                return this.btnMoveForward;
            }
            set
            {
                this.btnMoveForward = value;
                NotifyPropertyChanged("BtnMoveForward");
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


        private void FunctionBtnPlay(Object obj)
        {
            //we have to set true "IsEnabled", so video will start directly 
            IsPlaying(true);
            if (BtnPlayContent == "Play")
            {
                mediaPlayer.Play();
                BtnPlayContent = "Pause";
            }
            else
            {
                mediaPlayer.Pause();
                BtnPlayContent = "Play";
            }
        }

        private void FunctionBtnStop(Object obj)
        {
            mediaPlayer.Pause();
            btnPlayContent = "Play";
            IsPlaying(false);
            BtnPlay = true;
        }

        private void FunctionBtnMoveBack(Object obj)
        {
            mediaPlayer.Position -= TimeSpan.FromSeconds(10);
        }

        private void FunctionBtnMoveForward(Object obj)
        {
            //to fetch video time by TimeSpan.FromSeconds() function 
            mediaPlayer.Position += TimeSpan.FromSeconds(10);
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
                Uri uri = new Uri(dialog.FileName);
                mediaPlayer.Open(uri);

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
