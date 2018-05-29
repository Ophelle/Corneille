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
using ProjetCorneille.ViewModel;

namespace ProjetCorneille.ViewModel
{
    /// <summary>
    /// Logique d'interaction pour CreateCameraWindow.xaml
    /// </summary>
    public partial class CreateCameraWindow : Window
    {
        public CreateCameraWindow()
        {
            InitializeComponent();
            this.DataContext = new CreateCameraViewModel();
        }
    }
}
