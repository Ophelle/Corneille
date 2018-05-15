using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCorneille.Model
{
	class motion
	{
		public string Name { get; set; }
		public int ID { get; set; }
		public DateTime DateHours { get; set; }
		public List<Marker> Markers { get; set; }

		public class Marker
		{
			public int ID { get; set; }
			public string Start { get; set; }
			public string End { get; set; }
			public string Comment { get; set; }
		}
	}
}
