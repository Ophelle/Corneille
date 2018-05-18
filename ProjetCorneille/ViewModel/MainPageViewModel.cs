using System;
using System.ComponentModel;

namespace ProjetCorneille.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private string selectUserPath;

        public MainPageViewModel()
        {

        }

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
    }
}
