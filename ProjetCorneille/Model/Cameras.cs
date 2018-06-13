using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCorneille.Model
{
    // Objet caméras permettant de stocker les caméras
	public class Cameras
	{
		public List<Camera> CamerasList { get; set; }

		public class Camera
		{
			public string Name { get; set; }
			public int ID { get; set; }
			public List<Point> Coordinates { get; set; }
			public List<InfoMovie> InfoMovies { get; set; }

            // liste d'infoMovie pour pointer vers les XML des vidéos
			public class InfoMovie
			{
				public string PathMovie { get; set; }
			}
		}
	}
}
