//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------

using Microsoft.Xna.Framework.Graphics;

namespace LetsCreateNetworkGame
{
    public abstract class Component
    {
        private BaseObject _baseObject;
        public abstract ComponentType ComponentType { get; }
        
        public void Initialize(BaseObject baseObject)
        { 
            _baseObject = baseObject; 
        }

        public string GetOwnerId()
        {
            if (_baseObject == null)
                return ""; 
            return _baseObject.Username; 
        }

        public void RemoveMe()
        {
            _baseObject.RemoveComponent(this);
        }

        public void KillBaseObject()
        {
            _baseObject.Kill = true; 
        }

        public TComponentType GetComponent<TComponentType>(ComponentType componentType) where TComponentType : Component
        {
            return _baseObject == null ? null : _baseObject.GetComponent<TComponentType>(componentType);
        }

        public abstract void Update(double gameTime);
        public abstract void Draw(SpriteBatch spritebatch);

        public virtual void Initialize() { }

        public virtual void Uninitalize() { }

    }
}



