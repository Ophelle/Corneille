using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCorneille.Model
{
	class Cameras
	{
		public List<Camera> CamerasList { get; set; }

		public class Camera
		{
			public string Name { get; set; }
			public string ID { get; set; }
			public List<Coordinate> Coordinates { get; set; }
			public List<InfoMovie> InfoMovies { get; set; }

			public class Coordinate
			{
				public string ID { get; set; }
				public string X { get; set; }
				public string Y { get; set; }
			}

			public class InfoMovie
			{
				public string PathMovie { get; set; }
			}
		}
	}
}
