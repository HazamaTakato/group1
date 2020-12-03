using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Device;
using Oikake.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor
{
    class BoundAI:AI
    {
        private Vector2 velocity;//移動量

        public BoundAI()
        {
            var gameDevice = GameDevice.Instance();
            var rnd = gameDevice.GetRandom();
            int speed = rnd.Next(5, 21);
            speed = (rnd.Next(2) == 0) ? (speed) : (-speed);
            velocity = new Vector2(speed, 0.0f);
        }

        public override Vector2 Think(Character character)
        {
            //キャラクタの座標取得
            character.SetPosition(ref position);
            position = position + velocity;
            Range range = new Range(0, Screen.Width - 64);
            if (range.IsOutOfRange((int)position.X))
            {
                velocity = -velocity;
            }
            return position;
        }
    }
}
