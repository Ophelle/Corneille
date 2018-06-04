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
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Collections.ObjectModel;

namespace ProjetCorneille.ViewModel
{
    class WorkingMovieViewModel : ViewModelBase
    {
        private string pathOfMotionToXml;
        private string date;
        private bool startAndStop;
        private bool buttonDisableButton;
        private string category;
        private string comment;
        private bool buttonValVol0;
        private bool buttonValVol1;
        private bool buttonValVol2;
        private bool buttonValVol3;
        private Item valueSelectedMotion;
        private Item valueSelectedVideo;
        private ObservableCollection<Item> itemMotionList;
        private ObservableCollection<Item> itemList;
        private int motionIndex;
        private string nameOfMotionPath;

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
            CommandSaveMarqueur = new RelayCommand(FunctionSaveMarqueurToXml);

            ButtonValVol0 = true;
            StartAndStop = false;
            ButtonDisableButton = true;
            ItemList = new ObservableCollection<Item>();
            ItemMotionList = new ObservableCollection<Item>();
            sendVideoToCombobox();

            //Video Lecture
            CommandBtnPlay = new RelayCommand(FunctionBtnPlay);
            CommandBtnStop = new RelayCommand(FunctionBtnStop);
            CommandBtnMoveBack = new RelayCommand(FunctionBtnMoveBack);
            CommandBtnMoveForward = new RelayCommand(FunctionBtnMoveForward);
            CommandBtnOpen = new RelayCommand(FunctionBtnOpen);
            NextMotion = new RelayCommand(NextMotionItemList);
            PreviousMotion = new RelayCommand(PreviousMotionItemList);


            ButtonValVol0 = true;
            StartAndStop = false;
            BtnPlayContent = "Play";
            BtnStopContent = "Stop";
            BtnMoveBackContent = "Back";
            BtnMoveForwardContent = "Forward";
            BtnOpenContent = "Open";

            //For the video lecture it has to be false befor 
            IsPlaying(false);
            this.motionIndex = 0;
            //ValueSelectedMotion = (ItemList.Count > 0) ?  ItemMotionList[this.motionIndex] : null;
           

        }

        /*
         * Permet de pouvoir allez directement à la motion suivante
         * 
         */
         public void NextMotionItemList(Object obj)
        {           
            if(this.motionIndex + 1 < ItemMotionList.Count)
            {
                this.motionIndex++;
                ValueSelectedMotion = ItemMotionList[this.motionIndex];
            }
            
        }

         /*
         * Permet de pouvoir allez directement à la motion suivante
         * 
         */
        public void PreviousMotionItemList(Object obj)
        {
            if (this.motionIndex - 1 >= 0)
            {
                this.motionIndex--;
                ValueSelectedMotion = ItemMotionList[this.motionIndex];       
            }

        }

        private void IsPlaying(bool flag)
        {
            BtnPlay = true;
            BtnStop = flag;
            BtnMoveBack = flag;
            BtnMoveForward = flag;
            BtnOpen = true;

            CommandSaveMarqueur = new RelayCommand(FunctionSaveMarqueurToXml);

            ButtonValVol0 = true;
            StartAndStop = false;
            ButtonDisableButton = true;
           
        }

        // liste d item pour afficher les videos
        public ObservableCollection<Item> ItemList {
            get
            {
                return this.itemList;
            }
            set
            {
                this.itemList = value;
                NotifyPropertyChanged("ItemList");
            }
        }

        //Liste ditem motion afin de pouvoir afficher les motion d'une video
        public ObservableCollection<Item> ItemMotionList
        {
            get
            {
                return this.itemMotionList;
            }
            set
            {
                this.itemMotionList = value;
                NotifyPropertyChanged("ItemMotionList");
            }
        }

        //peupagle des motion via les videos 
        public void sendVideoToMotionCombobox(string NameOfVideo)
        {
            ObservableCollection<Item> video = XMLManager.bringMotionFromVideoAndXml(NameOfVideo);
            ItemMotionList = video;
        }

        // peupagle de la lsite de video
        /*
         * 
         * @Return list de video a afficher
         */

        public void sendVideoToCombobox()
        {
            ObservableCollection<Item> video = XMLManager.bringVideoFromXml();
            ItemList = video;
        }

        public RelayCommand CommandBtnPlay { get; set; }
        public RelayCommand CommandBtnStop { get; set; }
        public RelayCommand CommandBtnMoveBack { get; set; }

        public RelayCommand CommandBtnMoveForward { get; set; }
        public RelayCommand CommandBtnOpen { get; set; }

        // Acces to next Motion
        public RelayCommand NextMotion { get; set; }

        // Acces to previpous Motion
        public RelayCommand PreviousMotion { get; set; }



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
                
                Uri uri = new Uri(this.nameOfMotionPath);
                mediaPlayer.Open(uri);

                BtnPlay = true;
            }
        }

        //


        // Methode to change le statut 
        public void FunctionboolToChange(Object obj)
        {
            StartAndStop = true;
            ButtonDisableButton = false;
        }

        // Methode ta save marqueur
        public void FunctionSaveMarqueurToXml(Object obj)
        {
            if (!StartAndStop)
            {
                FunctionStartAndStopRecToXmlSaveMarqueur(this.pathOfMotionToXml, this.category, this.comment, this.date);
            }
            else
            {
                MessageBox.Show("Vous devez d'abord stopper le marqueur");
            }
           
        }


        // Methode to change le statut 
        public void FunctionboolToChangeToFalse(Object obj)
        {
            StartAndStop = false;
            ButtonDisableButton = true;
        }
        public bool ButtonDisableButton
        {
            get
            {
                return this.buttonDisableButton;
            }
            set
            {
                this.buttonDisableButton = value;
                NotifyPropertyChanged("ButtonDisableButton");
            }
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
        // Autres
        public bool ButtonValVol3
        {
            get
            {
                return this.buttonValVol3;
            }
            set
            {
                this.buttonValVol3 = value;
                NotifyPropertyChanged("buttonValVol3");
                this.category = "Autres";
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

        public Item ValueSelectedMotion
        {
            get
            {
                return this.valueSelectedMotion;
            }
            set
            {
                this.valueSelectedMotion = value;
                NotifyPropertyChanged("ValueSelectedMotion");
                try
                {
                    this.nameOfMotionPath = value.PathVideo;
                }
                catch
                {

                }
                
            }
        }

        public Item ValueSelectedVideo
        {
            get
            {
                return this.valueSelectedVideo;
            }
            set
            {
                try
                {
                    ValueSelectedMotion = ItemMotionList[0];
                }
                catch
                {

                }                
                this.valueSelectedVideo = value;                
                NotifyPropertyChanged("ValueSelectedVideo");
                sendVideoToMotionCombobox(value.PathVideo);
            }
        }

        public bool BtnPlayEnable
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

        /**
        * @bool paramètre permettant de savoir si on doit enregistrer ou pas la video 
        * @string category type de vol constaté 
        * @comment commentaire par le marqueur 
        * @string nom de la video 
        * @string nom de la motion
        * @DateTime heure de capture  
        *
        */
        public void FunctionStartAndStopRecToXmlSaveMarqueur(string nameOfMotion, string category, string comment, string date)
        {
            if(StartAndStop)
            {
                //Insestion dans les varaibles globales
                this.pathOfMotionToXml = nameOfMotion;
                this.category = category;
                this.comment = comment;
                this.date = date;
            }
            // Insertion dans le XMl et fin du marqueur
            else
            {
                this.comment = comment;
                this.category = category;
                XMLManager.addToXmlMarqueurMotionInMovie(this.category, Comment, this.pathOfMotionToXml, this.date, date);
                MessageBox.Show("Time du premier enregistrement" + this.date);
                MessageBox.Show("Time de fin enregistrement" + date);
                MessageBox.Show(this.pathOfMotionToXml);
                MessageBox.Show(this.category);
                MessageBox.Show(Comment);   
            }
        }            
    }
}

public class Item
{
    public int itemID { get; set; }
    public string Name { get; set; }
    public string PathVideo { get; set; }

    public Item(int ID, string name , string pathVideo)
    {
        this.itemID = ID;
        this.Name = name;
        this.PathVideo = pathVideo;
    }

}