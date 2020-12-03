using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Timers;

using Oikake.Def;
using Oikake.Scene;


namespace Oikake.Actor
{
    class bulletenemy : Character
    {
        private Vector2 velocity;//移動量
        private static Random rnd = new Random();
        private int time;
        private int interval;
        private int intervalTime=2;//敵の玉発射感覚
        public bulletenemy(IGameMediator mediator) : base("enemy1", mediator)
        {
            velocity = Vector2.Zero;
            position = Vector2.Zero;
        }

        public override void Initialize()
        { 
            position = new Vector2(rnd.Next(Screen.Width - 64),
                -64);
            //最初は上移動
            velocity = new Vector2(0, 1f);
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
                    new EnemyBullet(
                        position,
                        mediator,
                        velocity));

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
            isDeadFlag = true;//死亡
            mediator.AddScore(100);//得点追加
            //死んだら敵を増やす
            mediator.AddActor(new bulletenemy(mediator));
            mediator.AddActor(new bulletenemy(mediator));
            mediator.AddActor(new BurstEffect(position, mediator));//爆発エフェクトを追加
        }

        public Vector2 GetPosition()
        {
            Vector2 pos = position;
            return pos;
        }
    }
}