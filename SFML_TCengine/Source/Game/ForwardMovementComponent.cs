﻿using SFML.System;
using System.Diagnostics;
using TCEngine;

namespace TCGame
{
    // TODO (4): Create the ForwardMovementComponent
    //  - Although you can be creative, it should have at least these two memebers:
    //    - float m_Speed and Vector2f m_Forward
    //  - The main idea of this component is to move the actor in the m_Forward direction and the speed defined
    //    by the m_Speed member

    class ForwardMovementComponent : BaseComponent
    {
        float m_Speed;
        Vector2f m_Forward;
        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }

        public override object Clone()
        {

            return new ForwardMovementComponent();

        }

    }
}
