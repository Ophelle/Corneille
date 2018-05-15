using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace ProjetCorneille.Outils
{
    class OpencvTools
    {
        public static void swithOnTheCamera()
        {
            VideoCapture capture;
            Mat frame;
            capture = new VideoCapture();
            frame = new Mat();
            capture.Open(0);
            {
                Cv2.NamedWindow("Video", WindowMode.AutoSize);
                while (true)
                {
                    if (capture.Read(frame))
                    {
                        Cv2.ImShow("Video", frame);

                        // press a key to end execution
                        int c = Cv2.WaitKey(10);
                        if (c != -1) { break; } // Assuming image has focus
                    }
                }
            }
        }
    }
}
