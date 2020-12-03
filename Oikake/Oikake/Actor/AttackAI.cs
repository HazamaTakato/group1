using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Oikake.Actor
{
    class AttackAI:AI
    {
        private Character other;
        public AttackAI(Character other)
        {
            this.other = other;
        }

        public override Vector2 Think(Character character)
        {
            character.SetPosition(ref position);

            var otherPosition = Vector2.Zero;
            other.SetPosition(ref otherPosition);

            var velocity = otherPosition - position;
            velocity.Normalize();

            position += velocity * 5;

            return position;
        }
    }
}
