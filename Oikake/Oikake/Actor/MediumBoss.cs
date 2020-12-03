using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Util;
using Oikake.Scene;

namespace Oikake.Actor
{
    class MediumBoss : Character
    {
        
        private int hp;
        private Vector2 velocity;//移動量
        private static Random rnd = new Random();
        private int time;
        private int interval;
        private int intervalTime = 3;//敵の玉発射感覚
        public MediumBoss(IGameMediator mediator):base("tyuubosu",mediator)
        {
            velocity = Vector2.Zero;
            position = Vector2.Zero;
           
            hp = 15;
        }

        public void EmemyAppearane()
        {
            
            
            
        }
        public override void Hit(Character other)
        {
            isDeadFlag = true;//死亡
            mediator.AddScore(500);//得点追加
            mediator.AddActor(new BurstEffect(position, mediator));//爆発エフェクトを追加
        }

        public override void Initialize()
        {
            position = new Vector2(rnd.Next(800 - 128),
            rnd.Next(200, 400 - 128));
            //最初は上移動
            velocity = new Vector2(-10f, 0);
            interval = intervalTime * 60;
        }

        public override void Shutdown()
        {
            
        }

        public override void Update(GameTime gameTime)
        {

            time = time + 1;
            int a = time % interval;
            if (a == 0)
            {

                //弾を発射
                mediator.AddActor(
                    new MBossBallet(
                        position,
                        mediator,
                        new Vector2 (0,1f)));

            }
            int b = time % 300;
            if(b==0)
            {
                int c = rnd.Next(0, 4);
                if(c==0||c==2||c==3)
                {
                    mediator.AddActor(new bulletenemy(mediator));
                }
                else
                {
                    mediator.AddActor(new BonusEnemy(mediator));

                }

            }
            //左壁で反射
            if (position.X < 0)
            {
                //移動量を反射
                velocity = -velocity;
            }
            //右反射
            if (position.X > 800-128)
            {
                //移動量を反射
                velocity = -velocity;
            }
            //移動処理（座標に移動量を足す）
            position += velocity;
        }
        public Vector2 GetPosition()
        {
            Vector2 pos = position;
            return pos;
        }
    }
}
