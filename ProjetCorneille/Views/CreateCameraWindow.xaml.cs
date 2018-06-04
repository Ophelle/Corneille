using ProjetCorneille.Model;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjetCorneille.ViewModel
{
    /// <summary>
    /// Logique d'interaction pour CreateCameraWindow.xaml
    /// </summary>
    public partial class CreateCameraWindow : Window
    {
        PointCollection myPointCollection2;
        public CreateCameraWindow()
        {
            InitializeComponent();
            this.DataContext = new CreateCameraViewModel();
            myPointCollection2 = new PointCollection();
        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(tttt.Source.ToString());
            bitmapImage.EndInit();
            
            double X = e.GetPosition(tttt).X;
            double pixelMousePositionX = e.GetPosition(tttt).X * bitmapImage.PixelWidth / tttt.ActualWidth;
            double pixelMousePositionY = e.GetPosition(tttt).Y * bitmapImage.PixelHeight / tttt.ActualHeight;
            
            Polygon poly = new Polygon();
            poly.Stroke = Brushes.Red;
            poly.StrokeThickness = 1;
            poly.FillRule = FillRule.EvenOdd;
            myPointCollection2.Add(e.GetPosition(Cnv));
            
            poly.Points = myPointCollection2;
            Session.ZonePointList.Add(new Point(pixelMousePositionX, pixelMousePositionY));
            Cnv.Children.Add(poly);

        }

        private void CommandUndo(object sender, RoutedEventArgs e)
        {
            Polygon poly = new Polygon();
            poly.Stroke = Brushes.Red;
            poly.StrokeThickness = 1;
            poly.FillRule = FillRule.EvenOdd;
            if(myPointCollection2.Count > 0)
            {
                myPointCollection2.RemoveAt(myPointCollection2.Count - 1);
                poly.Points = myPointCollection2;
                Session.ZonePointList.RemoveAt(Session.ZonePointList.Count - 1);
            }
            Cnv.Children.Add(poly);
           
        }

        private void CommandReset(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Etes vous sure de vouloir remttre à zéro la selection ? ", "Confirmation", MessageBoxButton.YesNo);
            if(messageBoxResult == MessageBoxResult.Yes)
            {
                Polygon poly = new Polygon();
                poly.Stroke = Brushes.Red;
                poly.StrokeThickness = 1;
                poly.FillRule = FillRule.EvenOdd;
                myPointCollection2.Clear();
                poly.Points = myPointCollection2;
                Cnv.Children.Add(poly);
                Session.ZonePointList.Clear();
            }
           
        }
    }
}
