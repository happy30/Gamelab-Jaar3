//Conversation.cs by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;


public class Conversation
{
    [XmlAttribute("interactionCodeName")]
    public string interactionCodeName;

    [XmlArray("lines")]
    public Line[] lines;
}

public class Line
{
    [XmlElement("actors")]
    public Actors actors;

    [XmlElement("text")]
    public string text;

    [XmlElement("expression")]
    public Expression expression;

    [XmlElement("cameraPosition")]
    public Movement cameraPosition;

    [XmlElement("choice")]
    public Choice choice;

    [XmlElement("effect")]
    public Effect effects;

}

public class Expression
{
    
    public enum PortraitExpression
    {
        None,
        Neutral,
        Happy,
        Sad,
        Smirk,
        Thinking,
        Angry,
        Enraged,
        Frightened,
        Serious,
    };

    [XmlEnum("portraitExpression")]
    public PortraitExpression portraitExpression;

    [XmlElement("voiceActing")]
    public bool voiceActing;
}

public class Movement
{
    [XmlEnum("move")]
    public Move move;

    public enum Move
    {
        Middle,
        Left,
        Right,
    };
}

public class Actors
{
    [XmlEnum("actor")]
    public Actor actor;

    public enum Actor
    {
        Mind,
        Unknown,
        Professor,
        Anastasia,
        Client1,
        Client2,
        Client3,
        Client4,
        Client5,
        Client6,
        Client7,
        Doctor1,
        Assistant1,
        Assistant2,
        Chiaki,
        Hiyoko,
        Junko,
        MrRobot
    };
}

public class Choice
{
    [XmlElement("choice1")]
    public string choice1;

    [XmlElement("choice2")]
    public string choice2;

    [XmlElement("choice3")]
    public string choice3;

    [XmlElement("Destination1")]
    public string Destination1;

    [XmlElement("Destination2")]
    public string Destination2;

    [XmlElement("Destination3")]
    public string Destination3;
}

public class Effect
{
    public enum AdditionalEffect
    {
        None,
        Strobe,
        Black,
        GetItem,
        PickUp,
        CheckItem,
        AddObject,
        RemoveObject
    };


    [XmlEnum("Effect")]
    public AdditionalEffect effect;

    [XmlElement("EffectParameter")]
    public string effectParameter;

    [XmlElement("NewICN")]
    public string newInteractionCodeName;
}