﻿using System;
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
        public  static string nameOfMotionPath;

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
                this.date = "00.00.01";
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
                    this.pathOfMotionToXml = value.PathVideo;
                    nameOfMotionPath = value.PathVideo;
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