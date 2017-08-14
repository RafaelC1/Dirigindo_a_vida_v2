using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class XMLcrud : MonoBehaviour {

    public TextAsset XmlRawFile;

    private string[] texto = new string[5];

    string data;

    public int maxPerguntas;

    void Start()
    {
        _OpenXml();
    }

    public void _OpenXml()
    {
        this.data = XmlRawFile.text;
    }

    public string[] _GetTeInfo()
    {
        if(this.data == null)
        {
            _OpenXml();
        }

        XmlDocument xmldoc = new XmlDocument();

        xmldoc.Load(new StringReader(this.data));

        string xmlpathpattern = "//perguntas/p" + GetRandom().ToString();

        XmlNodeList mynodelist = xmldoc.SelectNodes(xmlpathpattern);

        foreach (XmlNode node in mynodelist)
        {

            XmlNode pergunta = node.FirstChild;
            XmlNode respA = pergunta.NextSibling;
            XmlNode respB = respA.NextSibling;
            XmlNode respC = respB.NextSibling;
            XmlNode respCorre = respC.NextSibling;

            texto[0] = pergunta.InnerText;
            texto[1] = respA.InnerText;
            texto[2] = respB.InnerText;
            texto[3] = respC.InnerText;
            texto[4] = respCorre.InnerText;
        }

        return texto;
    }

    int GetRandom()
    {
        int i = Random.Range(0,this.maxPerguntas);
        return i;
    }

}
