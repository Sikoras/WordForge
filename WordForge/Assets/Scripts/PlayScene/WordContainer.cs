using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("WordsCollection")]
public class WordContainer{

	[XmlArray("Words")]
    [XmlArrayItem("word")]
    public List<string> Words = new List<string>();


	public static WordContainer Load (string path)
	{
		var serializer = new XmlSerializer (typeof(WordContainer));
		using (var stream = new FileStream(path, FileMode.Open)) 
		{
			return serializer.Deserialize (stream) as WordContainer;
		}
	}

	public static WordContainer LoadFromText(string text)
	{
		var serializer = new XmlSerializer (typeof(WordContainer));
		return serializer.Deserialize (new StringReader (text)) as WordContainer;
	}
}
