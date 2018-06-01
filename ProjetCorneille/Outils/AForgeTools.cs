using System;
using System.Collections.Generic;
using System.Drawing;
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
        // Motion detect level for copy frame
        private static float _motionAlarmLevel = 0.001f;
        private static bool _hasMotion = false;
        private static int _volgnr;
        private static VideoFileWriter _FileWriter = new VideoFileWriter();
        private static VideoFileReader _FileReader = new VideoFileReader();
        private static DateTime _firstFrameTime = new DateTime();
        private static VideoSourcePlayer videoSourcePlayer;
        private static int nbPicture = 0;
        private static string fileNameMovie;

        public static void Initialisation(string path)
        {
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
                    _firstFrameTime = new DateTime();
                    // TODO : DateTime detect in the video 
                    XMLManager.CreateXMLMotion("/Movie/" + fileNameMovie + ".xml", _volgnr, DateTime.Now, DateTime.Now, "00", "00");
                }
                _FileReader.Close();
                _FileReader.Dispose();
            }
        }

        private static void VideoSourcePlayer_NewFrame(Bitmap image)
        {
            var motionLevel = _motionDetector.ProcessFrame(image);
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
                    _FileWriter.Open("/MotionsVideo/"+fileNameMovie+"_motion"+_volgnr+".avi", w, h, 25, VideoCodec.Default);
                    Console.WriteLine("/MotionsVideo/" + fileNameMovie + "_motion" + _volgnr + ".avi");
                    _FileWriter.WriteVideoFrame(image);
                    _firstFrameTime = DateTime.Now;
                }
                _hasMotion = true;
                nbPicture = 0;
            }
            else 
            {
                // Number of picture copy in the motion after motion alarm < _motionAlarmLevel 
                if (nbPicture > 200)
                {
                    Console.WriteLine(DateTime.Now + ": Motion stopped. Motion level: " + motionLevel);
                    _FileWriter.WriteVideoFrame(image);
                    _FileWriter.Close();
                    _FileWriter.Dispose();
                    nbPicture = 0;
                    _firstFrameTime = new DateTime();
                    // TODO : DateTime detect in the video 
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
    }
}
