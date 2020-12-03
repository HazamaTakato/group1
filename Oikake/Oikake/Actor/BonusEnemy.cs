using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor
{
    class BonusEnemy:Character
    {
        private Vector2 velocity;//移動量
        private Vector2 vel;//移動量２
        private static Random rnd = new Random();
        public BonusEnemy(IGameMediator mediator) : base("rea", mediator)
        {
        }

        public override void Initialize()
        {
            position = new Vector2(rnd.Next(800- 64),
                rnd.Next(100,200 - 64));
            //最初は左移動
            velocity = new Vector2(-10f, 0);
            vel = new Vector2(0, 1f);
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            //左壁で反射
            if (position.X < 0)
            {
                //移動量を反射
                velocity = -velocity;
            }
            //右反射
            if (position.X > 736)
            {
                //移動量を反射
                velocity = -velocity;
            }

            //移動処理（座標に移動量を足す）
            position += velocity;
        }   
        ///<summary>
        ///ヒット通知
        ///</summary>
        ///<param name="other">衝突した相手</param>
        public override void Hit(Character other)
        {
            isDeadFlag = true;
            if (isDeadFlag == true)
            {
                mediator.AddActor(
                    new DropItem(
                        position,
                        mediator,
                        vel));
            }
            mediator.AddActor(new BonusEnemy(mediator));
        }
    }
}
