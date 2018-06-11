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
        //Méthode permettant d'ouvrir un explorateur windows et de récupéré le chemin du fichier séléctionné
        public static string getPathUser()
        {
            string path = "";
            //Ouverture de l'explorateur windows
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string formats = "Fichiers image (*.avi, *.av, *.mp4, *.mov)|*.avi; *.av; *.mp4; *.mov|Tous les fichiers (*.*)|*.*";
            //Application des filtre de séléction de la vidéo
            openFileDialog.Filter = formats;
            //On vérifie qu'un fichié à bien été séléctionné
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }
            return path;
        }

        //Méthode permettant de de créer un apercu d'une image à partir d'une vidéo et la situe à l'endroit de la vidéo
        public static string createPreviewFromVideo(string pathVideo, string cameraName)
        {
            //Path de l'image qui sera créé par la suite
            string pathImage = "";

            if (!String.IsNullOrEmpty(pathVideo))
            {
                VideoFileReader reader = new VideoFileReader();
                //Ouvre la video
                reader.Open(pathVideo);
                //Lit la première image
                Bitmap videoFrame = reader.ReadVideoFrame();
                if (String.IsNullOrEmpty(cameraName))
                {
                    pathImage = Path.GetDirectoryName(pathVideo) + @"\" + Path.GetFileNameWithoutExtension(pathVideo) + "image.png";
                }
                else
                {
                    pathImage = Path.GetDirectoryName(pathVideo) + @"\" + cameraName + "image.png";
                }
                //Ecrase l'image si le nom est déjà pris
                if (File.Exists(pathImage))
                {
                    File.Delete(pathImage);

                }

                videoFrame.Save(pathImage);
                //On libère les ressources
                videoFrame.Dispose();
                reader.Close();
            }

            return pathImage;
        }
    }
}
