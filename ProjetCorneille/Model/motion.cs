using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCorneille.Model
{
	public class Motion
	{
		public string Name { get; set; }
		public DateTime DateHoursStart { get; set; }
        public DateTime DateHoursEnd { get; set; }
        public List<Marker> Markers { get; set; }

		public class Marker
		{
			public string Start { get; set; }
			public string End { get; set; }
			public string Comment { get; set; }
		}
	}
}
