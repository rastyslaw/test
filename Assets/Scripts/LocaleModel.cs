using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

public class LocaleModel
{
    //private Dictionary<string, XmlNode> xmls = new Dictionary<string, XmlNode>(); 
    private Dictionary<string, string> data = new Dictionary<string, string>();
    
    private LocaleModel instance;
    public LocaleModel Instance 
    {
        get
        {
            if (instance == null) 
            {
                instance = new LocaleModel();
            }
            return instance;
        }
    }

    private string locale;
    public string Locale
    {
        get { return locale; }
        set
        {
            locale = value;
          /*
            var xml:XML = xmls[_locale];

            if (xml == null)
            {
                errorHandler("LocaleManager: locale \"" + value + "\" not found");
                return;
            }
          
            for each(var textXML: XML in xml.children())

            {
                data[String(textXML.@id)] = String(textXML);
            }
            */
        }
    }

    public void AddLocale(XmlNode xml)
    {
		//xmls[String(xml.@name)] = xml;
    }

    public object GetString(string key)
    {
        if (data.ContainsKey(key))
        {
            return data[key];
        }
        return key; 
    }

    public string GetParamsString(string key, string[] args)
    {
        if (data.ContainsKey(key))
        {
            return replaceStringValues(data[key], args);  
        }
        return key;
    }

    public string replaceStringValues(string key, string[] args)
	{
        /*
        Regex regEx = /\{\w+\}/g;

		var matches:Array = str.match(r);

		for (var i:int = 0; i<matches.length; i++)
		{
			str = str.replace(matches[i], args[i]);
		}
		return unescape(str);
        */
        return null;

	}
}
