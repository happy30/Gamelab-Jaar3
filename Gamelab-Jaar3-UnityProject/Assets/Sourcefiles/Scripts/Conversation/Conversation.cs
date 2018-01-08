//Conversation.cs by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;


public class Conversation
{
    [XmlAttribute("icn")]
    public string icn;

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

    [XmlArray("choices")]
    public Choices[] choices;

    [XmlArray("effect")]
    public Effect[] effects;

}

public class Expression
{
    
    public enum PortraitExpression
    {
        None,
        Neutral,
        Excited,
        Laughing,
        Sad,
        Smirk,
        Thinking,
        Angry,
        Enraged,
        Frightened,
        Serious,
        Embarrassed,
        Shocked,
        Special,
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
        Dougal,
        Ana,
        Dust,
        Blaze,
        Livie,
        Violet,
        Scarlet,
        Marine,
        Moss,
        Auburn,
        Ash,
        Amber,
        Anastasia,
    };
}

public class Choices
{
    [XmlElement("choices")]
    public string choice;

    [XmlElement("destination")]
    public string destination;
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
        LinkNewConversation,
        ChangeInteractionCodeName,
        CheckItem,
        AddObject,
        RemoveObject,
        ProgressScene,
        ChangeToUse,
        EnterPuzzle,
        EndPuzzle,
        CheckProgression
    };


    [XmlEnum("Effect")]
    public AdditionalEffect effect;

    [XmlArray("Parameters")]
    public string[] parameter;

}