using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCorneille.Model
{
	public class Cameras
	{
		public List<Camera> CamerasList { get; set; }

		public class Camera
		{
			public string Name { get; set; }
			public int ID { get; set; }
			public List<Point> Coordinates { get; set; }
			public List<InfoMovie> InfoMovies { get; set; }

			public class InfoMovie
			{
				public string PathMovie { get; set; }
			}
		}
	}
}
