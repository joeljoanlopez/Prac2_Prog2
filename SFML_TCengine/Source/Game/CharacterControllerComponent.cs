﻿using System;
using System.Numerics;
using System.Diagnostics;
using SFML.System;
using SFML.Window;
using TCEngine;

namespace TCGame
{
    public class CharacterControllerComponent : BaseComponent
    {

        private const float MOVEMENT_SPEED = 200f;
        private CannonComponent cannonComponent;
        public CharacterControllerComponent()
        {
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return BaseComponent.EComponentUpdateCategory.PreUpdate;
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);

            // TODO (1.2): Implement the keyboard handling
            //   - Pressing W moves the Actor up
            //   - Pressing S moves the Actor down
            //   - Pressing A moves the Actor to the left
            //   - Pressing D moves the Actor to the right
            Vector2f movement = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                movement.Y -= 1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                movement.Y += 1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                movement.X -= 1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                movement.X += 1f;
            }
            movement = Normalize(movement);
            Vector2f displacement = movement * MOVEMENT_SPEED * _dt;
            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
            Debug.Assert(transformComponent != null);
            transformComponent.Transform.Position += displacement;


            // TODO (2.2): Implement the keyboard handling
            //   - Pressing Space shoots the cannon of this actor (only if this actor has a CannonComponent)
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                cannonComponent = Owner.GetComponent<CannonComponent>();
                Debug.Assert(cannonComponent != null);
                cannonComponent.Shoot();
            }

        }

        private Vector2f Normalize(Vector2f vec)
        {
            float _module = (float)Math.Sqrt(Math.Pow(vec.X, 2) + Math.Pow(vec.Y, 2));
            return _module != 0 ? vec / _module : vec;
        }
    }
}
