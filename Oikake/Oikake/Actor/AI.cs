using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor
{
    abstract class AI
    {
        protected Vector2 position;

        public AI()
        {
            position = Vector2.Zero;
        }
        public abstract Vector2 Think(Character character);
    }
}
