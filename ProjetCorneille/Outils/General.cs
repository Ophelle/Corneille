using Accord.Video.FFMPEG;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjetCorneille.Outils
{
    class General
    {

        public static string getPathUser()
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string formats = "Fichiers image (*.avi, *.mp4, *.mov)|*.avi; *.mp4; *.mov|Tous les fichiers (*.*)|*.*";
            openFileDialog.Filter = formats;
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }
            return path;
        }

        public static string createPreviewFromVideo(string pathVideo, string cameraName)
        {
            string pathImage = "";

            if (!String.IsNullOrEmpty(pathVideo))
            {
                VideoFileReader reader = new VideoFileReader();
                // open video file
                reader.Open(pathVideo);
                Bitmap videoFrame = reader.ReadVideoFrame();
                if (String.IsNullOrEmpty(cameraName))
                {
                    pathImage = Path.GetDirectoryName(pathVideo) + @"\" + Path.GetFileNameWithoutExtension(pathVideo) + "image.png";
                }
                else
                {
                    pathImage = Path.GetDirectoryName(pathVideo) + @"\" + cameraName + "image.png";
                }
                if (File.Exists(pathImage))
                {
                    File.Delete(pathImage);
                }
                videoFrame.Save(pathImage);
                videoFrame.Dispose();
                reader.Close();
            }

            return pathImage;
        }
    }
}
