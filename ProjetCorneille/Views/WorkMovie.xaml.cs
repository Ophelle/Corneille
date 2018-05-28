using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using ProjetCorneille.ViewModel;

namespace ProjetCorneille.Views
{
    /// <summary>
    /// Logique d'interaction pour WorkMovie.xaml
    /// </summary>
    public partial class WorkMovie : Window
    {
         
        public WorkMovie()
        {
            InitializeComponent();
            this.DataContext = new WorkingMovieViewModel();
        }

    }
}
