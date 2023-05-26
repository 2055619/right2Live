using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class LocalizationManager : MonoBehaviour
{
    XmlDocument xmlDoc = new ();
    XmlNodeList entryList;
    public static LocalizationManager instance = new ();
    public static Languages _currentLanguage = Languages.French;
    Dictionary<Languages, TextAsset> _localizationFiles = new ();
    Dictionary<string, string> _localizationData = new ();
    
    public enum Languages
    {
        English,
        French
    }
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        
        foreach(Languages language in Languages.GetValues(typeof(Languages)))
        {
            string textAssetPath = "Localization/" + language;
            TextAsset textAsset = (TextAsset)Resources.Load(textAssetPath);
            if (textAsset)
            {
                _localizationFiles[language] = textAsset;
            }
        }
        TextAsset textAsset1 = new TextAsset();
        if (_localizationFiles.ContainsKey(_currentLanguage))
        {
            textAsset1 = _localizationFiles[_currentLanguage];
        }
        xmlDoc.LoadXml(textAsset1.text);
        entryList = xmlDoc.GetElementsByTagName("Entry");
        foreach (XmlNode entry in entryList)
        {
            if(!_localizationData.ContainsKey(entry.FirstChild.InnerText))
            {
                _localizationData.Add(entry.FirstChild.InnerText, entry.LastChild.InnerText);
            }
        }
    }
    
    public string getEntry(string entry) {
        return _localizationData[entry];
    }
}
