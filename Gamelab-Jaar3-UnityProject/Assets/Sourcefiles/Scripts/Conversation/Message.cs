using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;

public class Message 
{
    [XmlElement("actor")]
    public string actor;

    [XmlElement("text")]
    public string text;

    [XmlElement("portaitExpression")]
    public string expression;

    [XmlElement("voiceActing")]
    public string voiceActing;

    [XmlElement("move")]
    public string move;

    [XmlElement("choice1")]
    public string choice1;

    [XmlElement("Destination1")]
    public string destination1;

    [XmlElement("choice2")]
    public string choice2;

    [XmlElement("Destination2")]
    public string destination2;

    [XmlElement("choice3")]
    public string choice3;

    [XmlElement("Destination3")]
    public string destination3;

    [XmlElement("additionalEffect")]
    public string effect;

}
