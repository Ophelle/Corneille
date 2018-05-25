using System;
using System.Collections.Generic;
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
        private static float _motionAlarmLevel = 0.05f;
        private static bool _hasMotion = false;
        private static int _volgnr;
        private static VideoFileWriter _FileWriter = new VideoFileWriter();
        private static DateTime _firstFrameTime = new DateTime();
        private static VideoSourcePlayer videoSourcePlayer;
        private static int nbPicture = 0;

        public static void Initialisation()
        {
            Console.WriteLine("Motion Detector");
            Console.WriteLine("Detects motion in the integrated laptop webcam");
            Console.WriteLine("Threshold level: " + _motionAlarmLevel);
            _motionDetector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionAreaHighlighting());
            if (new FilterInfoCollection(FilterCategory.VideoInputDevice).Count > 0)
            {

                if (!Directory.Exists("/MotionsVideo"))
                {
                    Directory.CreateDirectory("/MotionsVideo");
                }
                else
                {
                    var dir = new DirectoryInfo("/MotionsVideo");
                    foreach (var fi in dir.GetFiles())
                    {
                        fi.Delete();
                    }
                }

                var videoDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice)[0];
                var videoCaptureDevice = new VideoCaptureDevice(videoDevice.MonikerString);
                videoSourcePlayer = new VideoSourcePlayer();
                videoSourcePlayer.NewFrame += VideoSourcePlayer_NewFrame;
                videoSourcePlayer.VideoSource = new AsyncVideoSource(videoCaptureDevice);
                videoSourcePlayer.Start();
            }
        }

        private static void VideoSourcePlayer_NewFrame(object sender, ref System.Drawing.Bitmap image)
        {
            var motionLevel = _motionDetector.ProcessFrame(image);
            if (motionLevel > _motionAlarmLevel)
            {
                if (_hasMotion){
                    _FileWriter.WriteVideoFrame(image, DateTime.Now - _firstFrameTime);
                }
                else if (nbPicture == 0){
                    _volgnr++;
                    int h = image.Height;
                    int w = image.Width;
                    Console.WriteLine(DateTime.Now + ": Motion started. Motion level: " + motionLevel);
                    _FileWriter = new VideoFileWriter();
                    _FileWriter.Open("/MotionsVideo/motion"+_volgnr, w, h);
                    Console.WriteLine("/MotionsVideo/motion" + _volgnr);
                    _FileWriter.WriteVideoFrame(image);
                    _firstFrameTime = DateTime.Now;
                }
                _hasMotion = true;
                nbPicture = 0;
            }
            else 
            {
                if (nbPicture > 20)
                {
                    Console.WriteLine(DateTime.Now + ": Motion stopped. Motion level: " + motionLevel);
                    _FileWriter.WriteVideoFrame(image);
                    _FileWriter.Close();
                    _FileWriter.Dispose();
                    nbPicture = 0;
                    _firstFrameTime = new DateTime();
                }
                else if(_hasMotion || nbPicture >=1)
                {
                    _FileWriter.WriteVideoFrame(image, DateTime.Now - _firstFrameTime);
                    nbPicture++;
                }
                _hasMotion = false;
            }
        }

        public static void stopCamera()
        {
            if (videoSourcePlayer != null)
            {
                videoSourcePlayer.Stop();
                videoSourcePlayer = null;
            }

        }
    }
}
