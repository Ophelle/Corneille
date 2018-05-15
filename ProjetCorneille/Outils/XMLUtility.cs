using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjetCorneille.Outils
{
	class XMLUtility
	{
		public static T DeserializeForXml<T>(string filePath)
		{
			XmlSerializer selializer = new XmlSerializer(typeof(T));
			using (Stream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return (T)selializer.Deserialize(fs);
			}
		}

		public static void SerializeForXml<T>(string fileName, string type,Object XML)
		{
			XmlSerializer x = new XmlSerializer(typeof(T));
			TextWriter writer = new StreamWriter(type+"/"+fileName);
			x.Serialize(writer, XML);
		}
	}
}
