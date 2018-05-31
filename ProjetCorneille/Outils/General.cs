using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            string formats = "Fichiers image (*.avi, *.av, *.mp4, *.mov)|*.avi; *.av; *.mp4; *.mov|Tous les fichiers (*.*)|*.*";
            openFileDialog.Filter = formats;
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }
            MessageBox.Show(path);
            return path;
        }
    }
}
