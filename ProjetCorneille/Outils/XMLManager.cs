using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetCorneille.Model;
using static ProjetCorneille.Model.Cameras;
using static ProjetCorneille.Model.Cameras.Camera;
using static ProjetCorneille.Model.Movie;
using ProjetCorneille.ViewModel;
using System.Collections.ObjectModel;
using System.Drawing;

namespace ProjetCorneille.Outils
{
    class XMLManager
    {
        public static void CreateXMLMovie(string path, int idCamera)
        {
            string fileName;

            fileName = Path.GetFileNameWithoutExtension(path);
            Movie movie = new Movie();
            movie.IDCamera = idCamera;
            movie.Path = path;
            movie.Name = fileName;
            XMLUtility.SerializeForXml<Movie>(fileName, "Movie", movie);

            AddMovieInXMLCameras(idCamera, fileName);

        }

        internal static List<Point> ReadPolygonInXMLCamera(int idCamera)
        {
            Cameras cameras = XMLUtility.DeserializeForXml<Cameras>("/Cameras/Cameras.xml");
            return cameras.CamerasList.Where(cam => cam.ID == idCamera).Single().Coordinates;
        }

        private static void AddMovieInXMLCameras(int idCamera, string fileName)
        {
            Cameras cameras = XMLUtility.DeserializeForXml<Cameras>("/Cameras/Cameras.xml");
            InfoMovie infoMovie = new InfoMovie();
            infoMovie.PathMovie = "/Movie/"+fileName+".xml";
            cameras.CamerasList.Where(cam => cam.ID == idCamera).Single().InfoMovies.Add(infoMovie);
            XMLUtility.SerializeForXml<Cameras>("Cameras", "Cameras", cameras);
        }

        public static void CreateXMLCameras()
        {
            Cameras cameras = new Cameras();
            cameras.CamerasList = new List<Camera>();
            XMLUtility.SerializeForXml<Cameras>("Cameras", "Cameras", cameras);
        }

        public static void AddCameraInXMLCameras(string name, List<Point> coordinates)
        {
            int idBefore = 0;
            if (!File.Exists("/Cameras/Cameras.xml"))
            {
                CreateXMLCameras();
            }
            Cameras cameras = XMLUtility.DeserializeForXml<Cameras>("/Cameras/Cameras.xml");
            Camera camera = new Camera();
            camera.Name = name;
            if (cameras.CamerasList.Count > 0)
            {
                idBefore = cameras.CamerasList.Max(cam => cam.ID);
            }
            camera.ID = idBefore + 1;
            camera.Coordinates = coordinates;
            cameras.CamerasList.Add(camera);
            XMLUtility.SerializeForXml<Cameras>("Cameras", "Cameras", cameras);
        }

        public static int idLastCamera()
        {
            int id = 0;
            if (!File.Exists("/Cameras/Cameras.xml"))
            {
                CreateXMLCameras();
            }
            Cameras cameras = XMLUtility.DeserializeForXml<Cameras>("/Cameras/Cameras.xml");
            if(cameras.CamerasList.Count > 0)
            {
                id = cameras.CamerasList.Max(cam => cam.ID);
            }
            return id;
        }

        public static void CreateXMLMotion(string pathMovie, int numbermotion, DateTime dateHoursStart, DateTime dateHoursEnd, string start, string end)
        {
            string fileName = Path.GetFileNameWithoutExtension(pathMovie) + "_Number_" + numbermotion;
            Motion motion = new Motion();
            motion.Name = fileName;
            motion.DateHoursStart = dateHoursStart;
            motion.DateHoursEnd = dateHoursEnd;
            XMLUtility.SerializeForXml<Motion>(fileName, "Motion", motion);
            AddMotionInXMLMovie("/Motion/" + fileName, pathMovie, start, end);
        }

        private static void AddMotionInXMLMovie(string pathMotion, string pathMovie, string start, string end)
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
        
        public static void addToXmlMarqueurMotionInMovie(string category, string comment, string pathMotion, string date , string dateFin)
        {
            string fileName = Path.GetFileNameWithoutExtension(pathMotion);
            Motion motions = XMLUtility.DeserializeForXml<Motion>("/Motion/"+ fileName + ".xml");
            Motion.Marker addMotion = new Motion.Marker();
            addMotion.Start = date;
            addMotion.End = dateFin;
            addMotion.Action = category;
            addMotion.Comment = comment;
            motions.Markers.Add(addMotion);
            XMLUtility.SerializeForXml<Motion>(fileName, "Motion", motions);
        }

        public static ObservableCollection<Item> bringVideoFromXml()
        {
            ObservableCollection<Item> video = new ObservableCollection<Item>();
            // TODO faire le traitment pour alimenter la liste
            int numberOfMovie = 1;
            // Charger le xml de toute les cameras 
            Cameras cameras = XMLUtility.DeserializeForXml<Cameras>("/Cameras/Cameras.xml");
            // recupere tous les pathMovies, iterer sur camera 
            foreach(Camera CameraVideo in cameras.CamerasList)
            {
                foreach (InfoMovie infoMovie in CameraVideo.InfoMovies)
                {
                    string fileName;
                    fileName = Path.GetFileNameWithoutExtension(infoMovie.PathMovie);
                    Item videoPath = new Item(numberOfMovie, fileName , infoMovie.PathMovie);
                    numberOfMovie++;
                    video.Add(videoPath);                
                }
            }           
            // boucluer sur info movie 
            return video;
        }

        public static ObservableCollection<Item> bringCameraFromXml()
        {
            ObservableCollection<Item> camerasList = new ObservableCollection<Item>();
            // Charger le xml de toute les cameras
            Cameras cameras = XMLUtility.DeserializeForXml<Cameras>("/Cameras/Cameras.xml");
            // recupere toutes les cameras
            foreach (Camera CameraVideo in cameras.CamerasList)
            {
                Item camerasItem = new Item(CameraVideo.ID, CameraVideo.Name, null);
                camerasList.Add(camerasItem);
            } 
            return camerasList;
        }

        public static ObservableCollection<Item> bringMotionFromVideoAndXml(string pathOfVideo)
        {
            ObservableCollection<Item> video = new ObservableCollection<Item>();
            // TODO faire le traitment pour alimenter la liste
            Movie movie = XMLUtility.DeserializeForXml<Movie>(pathOfVideo);
            int numberOfMotion = 1;

            foreach (InfoMotion movieInfoMotion in movie.InfoMotions)
            {
                string fileName;
                fileName = Path.GetFileNameWithoutExtension(movieInfoMotion.PathMotion);
                Item motionPath = new Item(numberOfMotion, fileName, "MotionsVideo/"+fileName+".avi");
                numberOfMotion++;
                video.Add(motionPath);
            }
            return video;
        }
    }
}
