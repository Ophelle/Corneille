using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetCorneille.Model;
using ProjetCorneille.Outils;
using static ProjetCorneille.Model.Cameras;
using static ProjetCorneille.Model.Cameras.Camera;
using static ProjetCorneille.Model.Movie;

namespace ProjetCorneille.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string selectUserPath;

        public MainPageViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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
                this.NotifyPropertyChanged("SelectUserPath");
            }
        }

        public void CreateXMLMovie(string path, int idCamera)
        {
            string fileName;
    
            fileName = Path.GetFileNameWithoutExtension(path);
            Movie movie = new Movie();
            movie.IDCamera = idCamera;
            movie.Path = path;
            movie.Name = fileName;
            XMLUtility.SerializeForXml<Movie>(fileName,"Movie",movie);

            AddMovieInXMLCameras(idCamera, path);

        }

        private void AddMovieInXMLCameras(int idCamera, string path)
        {
            Cameras cameras = XMLUtility.DeserializeForXml<Cameras>("/Cameras/Cameras.xml");
            InfoMovie infoMovie = new InfoMovie();
            infoMovie.PathMovie = path;
            cameras.CamerasList.Where(cam => cam.ID == idCamera).Single().InfoMovies.Add(infoMovie);
            XMLUtility.SerializeForXml<Movie>("Cameras", "Cameras", cameras);
        }

        public void CreateXMLCameras()
        {
            Cameras cameras = new Cameras();
            XMLUtility.SerializeForXml<Movie>("Cameras", "Cameras", cameras);
        }

        public void AddCameraInXMLCameras(string name, List<Coordinate> coordinates)
        {
            if (!File.Exists("/Cameras/Cameras.xml"))
            {
                CreateXMLCameras();
            }
            Cameras cameras = XMLUtility.DeserializeForXml<Cameras>("/Cameras/Cameras.xml");
            Camera camera = new Camera();
            camera.Name = name;
            int idBefore = cameras.CamerasList.Max(cam => cam.ID);
            camera.ID = idBefore+1;
            camera.Coordinates = coordinates;
            cameras.CamerasList.Add(camera);
            XMLUtility.SerializeForXml<Movie>("Cameras", "Cameras", cameras);
        }

        public void CreateXMLMotion(string pathMovie,int numbermotion, DateTime dateHoursStart, DateTime dateHoursEnd, string start, string end)
        {
            string fileName = Path.GetFileNameWithoutExtension(pathMovie)+"_Number_"+ numbermotion;
            Motion motion = new Motion();
            motion.Name = fileName;
            motion.DateHoursStart = dateHoursStart;
            motion.DateHoursEnd = dateHoursEnd;
            XMLUtility.SerializeForXml<Movie>(fileName, "Motion", motion);

            AddMotionInXMLMovie("/Motion/"+fileName, pathMovie, start, end);

        }

        private void AddMotionInXMLMovie(string pathMotion, string pathMovie, string start, string end)
        {
            string fileName;

            fileName = Path.GetFileNameWithoutExtension(pathMovie);
            Movie movie = XMLUtility.DeserializeForXml<Movie>(pathMovie);
            InfoMotion infoMotion = new InfoMotion();
            infoMotion.PathMotion = pathMotion;
            infoMotion.Start = start;
            infoMotion.End = end;
            movie.InfoMotions.Add(infoMotion);
            XMLUtility.SerializeForXml<Movie>(fileName, "Movie", movie);
        }
    }
}
