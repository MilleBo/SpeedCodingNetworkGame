//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LetsCreateNetworkGame.Library
{
    public class Entity
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ScreenXPosition { get; set; }
        public int ScreenYPosition { get; set; }
        public bool Visible { get; set; }

        public Entity(int xPosition, int yPosition, int screenXPosition, int screenYPosition, bool visible)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            ScreenXPosition = screenXPosition;
            ScreenYPosition = screenYPosition;
            Visible = visible; 
        }

        public Entity() { }
    }
}
