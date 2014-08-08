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
using Microsoft.Xna.Framework.Input;

namespace LetsCreateNetworkGame.Manager
{
    class ManagerInput
    {
        private ManagerNetwork _managerNetwork; 
        public ManagerInput(ManagerNetwork managerNetwork)
        {
            _managerNetwork = managerNetwork; 
        }

        public void Update(double gameTime)
        {
            var state = Keyboard.GetState(); 
            CheckKeyState(Keys.Down,state);
            CheckKeyState(Keys.Up, state);
            CheckKeyState(Keys.Left, state);
            CheckKeyState(Keys.Right, state);
        }

        private void CheckKeyState(Keys key, KeyboardState state)
        {
            if (state.IsKeyDown(key))
            {
                _managerNetwork.SendInput(key); 
            }
        }
    }
}
