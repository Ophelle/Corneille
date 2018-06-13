using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCorneille.Model
{
    // objet permettant de créer les XML vidéos
	public class Movie
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public List<InfoMotion> InfoMotions { get; set; }
		public int IDCamera{ get; set; }

        // Liste des infoMotion qui permet de retrouver les XML des motion d'une vidéo 
		public class InfoMotion
		{
			public string PathMotion { get; set; }
			public string Start { get; set; }
			public string End { get; set; }
		}
	}
}
