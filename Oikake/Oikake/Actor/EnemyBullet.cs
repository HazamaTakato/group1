using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor
{
    class EnemyBullet:Character
    {
        private Vector2 velocity;
        
        public EnemyBullet(Vector2 position, IGameMediator mediator, Vector2
            velocity)
            : base("enemy1_bullet", mediator)
        {
            this.position = position;
            this.velocity = velocity;
        }
        public override void Hit(Character other)
        {
            //if (other is PlayerBullet)
            //{
            //if(other is DropItem)
            //{
            //    isDeadFlag = false;
            //}
                isDeadFlag = true;
            if(other is Player)
            {
                mediator.AddScore(-100);
            }
        }
        public override void Initialize()
        {
            
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            //移動処理
            position += velocity * 15;

            //画面の外に出たら死亡
            Range range = new Range(0, Screen.Width);
            if (range.IsOutOfRange((int)position.X))
            {
                isDeadFlag = true;
            }
            range = new Range(0, Screen.Height);
            if (range.IsOutOfRange((int)position.Y))
            {
                isDeadFlag = true;
            }
        }
    }
}
