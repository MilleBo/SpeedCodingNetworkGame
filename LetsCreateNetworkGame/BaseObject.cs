//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace LetsCreateNetworkGame
{
    public class BaseObject
    {
        public string Username { get; set; }
        public bool Kill { get; set; }

        private readonly List<Component> _components; 

        public BaseObject()
        {
            _components = new List<Component>();
            Kill = false; 
        }

        public TComponentType GetComponent<TComponentType>(ComponentType componentType) where TComponentType : Component
        {
            return _components.Find(c => c.ComponentType == componentType) as TComponentType; 
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);
            component.Initialize(this);
        }

        public void AddComponent(List<Component> components)
        {
            _components.AddRange(components);
            foreach (var component in components)
            {
                component.Initialize(this);
            }
        }

        public void RemoveComponent(Component component)
        {
            _components.Remove(component);

        }

        public virtual void Update(double gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {
                component.Draw(spriteBatch);
            }
        }


        public void Initialize()
        {
            if (_components == null)
                return; 
            _components.ForEach(c => c.Initialize());
        }

        public void Uninitialize()
        {
            if (_components == null)
                return; 
            _components.ForEach(c => c.Uninitalize());
        }
    }
}



