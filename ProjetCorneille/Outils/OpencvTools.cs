using System;
using System.Collections;
using System.Collections.Generic;
using OpenCvSharp;

namespace ProjetCorneille.Outils
{
    class OpencvTools
    {
        public static void openVideoFile1(string path)
        {
            VideoCapture capture;
            Mat frame;
            capture = new VideoCapture();
            frame = new Mat();
            capture.Open(path);
            while (capture.Read(frame))
            {
                
                Cv2.ImShow("VIdéo",  frame);
                if (Cv2.WaitKey(1) != -1) {
                    break;
                        };
            }
            Cv2.DestroyAllWindows();
        }


        public static void openVideoFile(string path)
        {


            double fps = 23;
            //from video
            var cap = new VideoCapture(path);

            int sleepTime = (int)Math.Round(1000 / fps);
            using (Window window = new Window("capture"))
            using (Mat image = new Mat())
            {
                while (true)
                {
                    cap.Read(image);
                    if (image.Empty())
                        break;
                    var img = image.Clone();
                    img = FindObject(img);

                    window.ShowImage(img);
                    if (Cv2.WaitKey(1) != -1)
                    {
                        break;
                    };
                    img.Release();
                }
            }
            cap.Release();

        }

        public static void swithOnTheCamera()
        {

            double fps = 30;
            //from video
            var cap = new VideoCapture(0);

            int sleepTime = (int)Math.Round(1000 / fps);
            using (Window window = new Window("capture"))
            using (Mat image = new Mat())
            {
                while (true)
                {
                    cap.Read(image);
                    if (image.Empty())
                        break;
                    var img = image.Clone();
                    img = FindObject(img);

                    

                    window.ShowImage(img);
                    if (Cv2.WaitKey(1) != -1)
                    {
                        break;
                    };
                    img.Release();
                }
            }
            cap.Release();





        }



        // Return un objet retrouver dans l'image 

       public static Mat FindObject(Mat src)
        {
            // Definition d'une couleur pour le traitement de la video sur des ton de niveau de gris 
            var gray = new Mat();
            // Premier parametre la source , le deuxieme le rendue et le troisieme de rendue 
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            var blur = new Mat();
            Cv2.GaussianBlur(gray, blur, new OpenCvSharp.Size(5, 5), 0);

            // Debut de la région



            var canny = new Mat();
            Cv2.Canny(blur, canny, 100, 500, 3, false);
            Cv2.ImShow("canny", canny);
            // Fin de la region 

            // Améliorer les contour 
            Cv2.Dilate(canny, canny, new Mat(), null, 1);
            Cv2.ImShow("dialte", canny);

            // region find contours
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchyIndexes;
            Cv2.FindContours(
                canny,
                out contours,
                out hierarchyIndexes,
                mode: RetrievalModes.External,
                method: ContourApproximationModes.ApproxSimple);
            // end region

            List<OpenCvSharp.Rect> rectList = new List<OpenCvSharp.Rect>();
            foreach (var c in contours)
            {
                //skip too small obj
                if (c.Length > 15 )
                    rectList.Add(Cv2.BoundingRect(c));
            }

            rectList = MergeOverlapRect(rectList);
            foreach (var rect in rectList)
            {
                Cv2.Rectangle(src, new OpenCvSharp.Point(rect.X, rect.Y), new OpenCvSharp.Point(rect.X + rect.Width, rect.Y + rect.Height), Scalar.Red, 2);
            }

            gray.Release();
            canny.Release();
            blur.Release();
            return src;
        }
 
        


        //Merge overlap rect
        private static List<OpenCvSharp.Rect> MergeOverlapRect(List<OpenCvSharp.Rect> list, double overlap = 0.5)
        {
            var filter = new List<OpenCvSharp.Rect>();
            for (int i = 0; i < list.Count; i++)
            {
                var flag = true;
                for (int j = 0; j < list.Count; j++)
                {
                    if (i == j) continue;
                    if (list[j].Contains(list[i]))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) filter.Add(list[i]);
            }

            for (int i = 0; i < filter.Count; i++)
            {
                for (int j = i + 1; j < filter.Count; j++)
                {
                    var a = filter[i];
                    var b = filter[j];
                    if (!a.IntersectsWith(b)) continue;
                    var intersect = a.Intersect(b);
                    if (CalSize(intersect.Size, a.Size) > overlap || CalSize(intersect.Size, b.Size) > overlap)
                    {
                        filter[i] = filter[i].Union(filter[j]);
                        filter.Remove(filter[j--]);
                    }
                }
            }
            return filter;
        }

        //Return overlap size
        private static double CalSize(OpenCvSharp.Size a, OpenCvSharp.Size b)
        {
            return a.Height * a.Width / (double)(b.Height * b.Width);
        }

    }


}

