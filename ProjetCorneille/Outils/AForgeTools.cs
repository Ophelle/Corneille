using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Video.FFMPEG;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;


namespace ProjetCorneille.Outils
{
    class AForgeTools
    {
        private static MotionDetector _motionDetector;
        // Niveau de la detection de mouvements, plus il est faible plus le mouvement petit sera acceptable
        private static float _motionAlarmLevel = 0.001f;
        private static bool _hasMotion = false;
        private static int _volgnr;
        private static VideoFileWriter _FileWriter = new VideoFileWriter();
        private static VideoFileReader _FileReader = new VideoFileReader();
        private static int nbPicture = 0;
        private static string fileNameMovie;
        private static List<Point> polygon;
        private static Bitmap bm;

        // Méthode qui lit la vidéo et lance un algorithme de traitement
        public static void Initialisation(string path, List<Point> polygon_img)
        {
            polygon = polygon_img;
            _FileReader = new VideoFileReader();
            Console.WriteLine("Motion Detector");
            Console.WriteLine("Threshold level: " + _motionAlarmLevel);
            _motionDetector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionAreaHighlighting());
            fileNameMovie = Path.GetFileNameWithoutExtension(path);

            if (File.Exists(path))
            {

                if (!Directory.Exists("/MotionsVideo"))
                {
                    Directory.CreateDirectory("/MotionsVideo");
                }

                _FileReader.Open(path);
                //lecture de la vidéo image par image
                while (true){
                    using (Bitmap videoFrame = _FileReader.ReadVideoFrame())
                    {
                        if (videoFrame == null)
                            break;
                        else
                            VideoSourcePlayer_NewFrame(videoFrame);
                    }
                }
                if (nbPicture >= 1)
                {
                    Console.WriteLine(DateTime.Now + ": Video stopped");
                    _FileWriter.Close();
                    _FileWriter.Dispose();
                    nbPicture = 0;
                    // TODO : la date réelle sur la video à mettre à la place de "00" 
                    XMLManager.CreateXMLMotion("/Movie/" + fileNameMovie + ".xml", _volgnr, DateTime.Now, DateTime.Now, "00", "00");
                }
                _FileReader.Close();
                _FileReader.Dispose();
            }
        }

        // Méthode d'analyse de l'image pour trouver un mouvement
        private static void VideoSourcePlayer_NewFrame(Bitmap image)
        {
            Bitmap img_cut = SelectedZone(image, polygon);
            // récupère le niveau de mouvement
            var motionLevel = _motionDetector.ProcessFrame(img_cut);
            img_cut.Dispose();
            if (motionLevel > _motionAlarmLevel)
            {
                if (_hasMotion){
                    _FileWriter.WriteVideoFrame(image);
                }
                else if (nbPicture == 0){
                    _volgnr++;
                    int h = image.Height;
                    int w = image.Width;
                    Console.WriteLine(DateTime.Now + ": Motion started. Motion level: " + motionLevel);
                    _FileWriter = new VideoFileWriter();
                    // création de la vidéo motion en .av, le nombre d'image par seconde peut être réglé
                    _FileWriter.Open("/MotionsVideo/"+fileNameMovie+"_Number_"+_volgnr+".avi", w, h, 25, VideoCodec.Default);
                    Console.WriteLine("/MotionsVideo/" + fileNameMovie + "_Number_" + _volgnr + ".avi");
                    _FileWriter.WriteVideoFrame(image);
                }
                _hasMotion = true;
                nbPicture = 0;
            }
            else 
            {
                // nombre d'image que l'on garde dans la video à la fin d'un mouvement peut être réglé.  
                if (nbPicture > 200)
                {
                    Console.WriteLine(DateTime.Now + ": Motion stopped. Motion level: " + motionLevel);
                    _FileWriter.WriteVideoFrame(image);
                    _FileWriter.Close();
                    _FileWriter.Dispose();
                    nbPicture = 0;
                    // TODO : la date réelle sur la video à mettre à la place de "00" 
                    XMLManager.CreateXMLMotion("/Movie/"+fileNameMovie+".xml", _volgnr, DateTime.Now, DateTime.Now, "00", "00");
                }
                else if(_hasMotion || nbPicture >=1)
                {
                    _FileWriter.WriteVideoFrame(image);
                    nbPicture++;
                }
                _hasMotion = false;
            }
        }

        //Méthode qui met en transparent les pixels hors d'un polygone (liste de points)
        private static Bitmap SelectedZone (Bitmap img, List<Point> points)
        {
            // copie de l'image initiale pour le pas la changer 
            bm = (Bitmap)img.Clone();
            // Met en transparent les pixels hors du polygone
            using (Graphics gr = Graphics.FromImage(bm))
            {
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(points.ToArray());
                // Sélectionne une zone en excluant l'exterieur du polygone
                gr.SetClip(path, CombineMode.Exclude);
                // Met en transparant la sélection
                gr.Clear(Color.Transparent);
                gr.ResetClip();
            }
            return bm;
        }
    }
}
