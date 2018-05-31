using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ProjetCorneille.Model;

namespace ProjetCorneille.Outils
{
	class XMLUtility
	{
		public static T DeserializeForXml<T>(string filePath)
		{
			XmlSerializer selializer = new XmlSerializer(typeof(T));
			using (Stream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
			{
				return (T)selializer.Deserialize(fs);
			}
		}

		public static void SerializeForXml<T>(string fileName, string type,Object XML)
		{
			XmlSerializer x = new XmlSerializer(typeof(T));
            if (!Directory.Exists("/"+type))
            {
                Directory.CreateDirectory("/"+type);
            }
            TextWriter writer = new StreamWriter("/"+type+"/"+fileName+ ".xml");
			x.Serialize(writer, XML);
            writer.Close();
		}
	}
}
