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
    public Line[] lines;

    public class Line
    {
        public Actors actor;
        public string text;
        public Expression expression;
        public Movement cameraPosition;
        public Choice choice;
        


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
            public PortraitExpression portraitExpression;
            public bool voiceActing;
        }

        public class Movement
        {
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
            public Actor actor;
            public enum Actor
            {
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
            };
        }

        public class Choice
        {
            public string choice1;
            public string choice2;
            public string choice3;

            public string Destination1;
            public string Destination2;
            public string Destination3;
        }
    }

}
