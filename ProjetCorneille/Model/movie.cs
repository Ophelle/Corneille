using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCorneille.Model
{
	public class Movie
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public List<InfoMotion> InfoMotions { get; set; }
		public int IDCamera{ get; set; }

		public class InfoMotion
		{
			public string PathMotion { get; set; }
			public string Start { get; set; }
			public string End { get; set; }
		}
	}
}
