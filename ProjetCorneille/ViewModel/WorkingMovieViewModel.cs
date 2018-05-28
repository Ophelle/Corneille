using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjetCorneille.Outils;

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

        public WorkingMovieViewModel()
        {
            CommandButtonStart = new RelayCommand(FunctionboolToChange);
            CommandButtonStop = new RelayCommand(FunctionboolToChangeToFalse);
            CommandSaveMarqueur = new RelayCommand(FunctionboolToChange);
            ButtonValVol0 = true;
            StartAndStop = false;
        }

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
