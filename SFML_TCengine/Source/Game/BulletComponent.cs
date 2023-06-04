using System.Numerics;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Diagnostics;
using TCEngine;

namespace TCGame
{
    public class BulletComponent : BaseComponent
    {
        private List<ECollisionLayers> m_ImpactLayers;

        public BulletComponent()
        {
            m_ImpactLayers = new List<ECollisionLayers>();
        }

        public BulletComponent(List<ECollisionLayers> _impactLayers)
        {
            m_ImpactLayers = _impactLayers;
        }


        public void AddImpactLayer(ECollisionLayers _layer)
        {
            m_ImpactLayers.Add(_layer);
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);

            List<BoxCollisionComponent> collisionLayerComponents = TecnoCampusEngine.Get.Scene.GetAllComponents<BoxCollisionComponent>();
            foreach (BoxCollisionComponent collisionLayerComponent in collisionLayerComponents)
            {
                ECollisionLayers layer = collisionLayerComponent.Layer;
                if (m_ImpactLayers.Contains(layer))
                {
                    if (IsActorInRange(collisionLayerComponent.Owner))
                    {
                        // TODO (6.2): Destroy both actors, the bullet actor and the collision layer actor
                        Owner.Destroy();
                        collisionLayerComponent.Owner.Destroy();
                    }
                }
            }
        }

        private bool IsActorInRange(Actor _actor)
        {

            // TODO (6.1): Implement this method. It returns true if this actor is touching the _actor passed by parameter
            SpriteComponent thisCollision = Owner.GetComponent<SpriteComponent>();
            Transformable thisPosition = Owner.GetComponent<TransformComponent>().Transform;
            Debug.Assert(thisCollision != null);
            FloatRect thisRectangle = thisCollision.GetGlobalbounds();
            thisRectangle.Left = thisPosition.Position.X - thisPosition.Origin.X;
            thisRectangle.Top = thisPosition.Position.Y - thisPosition.Origin.Y;

            BoxCollisionComponent otherCollision = _actor.GetComponent<BoxCollisionComponent>();
            Transformable otherPosition = _actor.GetComponent<TransformComponent>().Transform;
            Debug.Assert(otherCollision != null);
            FloatRect otherRectangle = _actor.GetComponent<BoxCollisionComponent>().GetGlobalBounds();
            otherRectangle.Left = otherPosition.Position.X - thisPosition.Origin.X;
            otherRectangle.Top = otherPosition.Position.Y - thisPosition.Origin.Y;

            return thisRectangle.Intersects(otherRectangle);

            // Debug.Assert(_otherColComponent != null);
            // if (_thisCollision.GetGlobalbounds().Intersects(_otherColComponent.GetGlobalBounds()) && ! Owner.Equals(_actor)) 
            //     intersects = true;
            // return intersects;
        }

        public override object Clone()
        {
            BulletComponent clonedComponent = new BulletComponent(m_ImpactLayers);
            return clonedComponent;
        }
    }
}
