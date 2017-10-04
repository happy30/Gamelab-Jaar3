using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class SerializedConversation
{
    [XmlElement(ElementName = "lines")]
    public Lines lines { get; set; }
    [XmlAttribute(AttributeName = "interactionCodeName")]
    public string interactionCodeName { get; set; }

    [XmlRoot(ElementName = "actors")]
    public class Actors
    {
        [XmlElement(ElementName = "actor")]
        public string Actor { get; set; }
    }

    [XmlRoot(ElementName = "expression")]
    public class Expression
    {
        [XmlElement(ElementName = "portraitExpression")]
        public string PortraitExpression { get; set; }
        [XmlElement(ElementName = "voiceActing")]
        public string VoiceActing { get; set; }
    }

    [XmlRoot(ElementName = "cameraPosition")]
    public class CameraPosition
    {
        [XmlElement(ElementName = "move")]
        public string Move { get; set; }
    }

    [XmlRoot(ElementName = "choice")]
    public class Choice
    {
        [XmlElement(ElementName = "choice1")]
        public string Choice1 { get; set; }
        [XmlElement(ElementName = "choice2")]
        public string Choice2 { get; set; }
        [XmlElement(ElementName = "choice3")]
        public string Choice3 { get; set; }
        [XmlElement(ElementName = "Destination1")]
        public string Destination1 { get; set; }
        [XmlElement(ElementName = "Destination2")]
        public string Destination2 { get; set; }
        [XmlElement(ElementName = "Destination3")]
        public string Destination3 { get; set; }
    }

    [XmlRoot(ElementName = "Line")]
    public class Line
    {
        [XmlElement(ElementName = "actors")]
        public Actors Actors { get; set; }
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
        [XmlElement(ElementName = "expression")]
        public Expression Expression { get; set; }
        [XmlElement(ElementName = "cameraPosition")]
        public CameraPosition CameraPosition { get; set; }
        [XmlElement(ElementName = "choice")]
        public Choice Choice { get; set; }
        [XmlElement(ElementName = "additionalEffect")]
        public string AdditionalEffect { get; set; }
    }

    [XmlRoot(ElementName = "lines")]
    public class Lines
    {
        [XmlElement(ElementName = "Line")]
        public List<Line> Line { get; set; }
    }
}
