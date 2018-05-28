﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if(this.StartAndStop)
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
                XMLManager.addToXmlMarqueurMotionInMovie(this.nameOfMotion, this.nameOfVideo, this.date, date);
            }
        }            
    }
}
