
using Microsoft.Xna.Framework;//vector2用
using Oikake.Def;
using Oikake.Device;
using Oikake.Scene;
using Oikake.Util;
using System;

namespace Oikake.Actor
{
    class zako3 : Character
    {
        //private AI ai;
        private Random rnd;
        private float speed = 1;
        private int flame;
        private int intervalTime = 1 * 30;
        private Vector2 vel1;
        private Vector2 vel2;
        private int hp = 5;
        //private void Death()
        //{
        //    isDeadFlag = true;
        //    mediator.AddActor(new BurstEffect(position, mediator));
        //}
        public zako3(IGameMediator mediator)
            : base("teki3", mediator)
        {
            //this.ai = ai;
        }
        public override void Initialize()
        {
            var gameDevice = GameDevice.Instance();
            rnd = gameDevice.GetRandom();
            //position = new Vector2(
            //    rnd.Next(Screen.Width - 64),
            //    rnd.Next(Screen.Height - 64));

            position = new Vector2(rnd.Next(Screen.Width - 64), -64);
            vel1 = new Vector2(0f, -1f);
            vel2 = new Vector2(0f, 1f);
        }
        public override void Update(GameTime gameTime)
        {
            flame += 1;
            int ans = flame % intervalTime;
            if (ans == 0)
            {
                mediator.AddActor(
                    new laser(
                        position,
                        mediator,
                        vel1));
                mediator.AddActor(
                    new laser(
                        position,
                        mediator,
                        vel2));

            }
            position = position + new Vector2(0f, 1f) * speed;
        }
        public override void Shutdown()
        {

        }
        public override void Hit(Character other)
        {
            //int score = 0;
            //if (ai is BoundAI)
            //{
            //    score = 100;
            //}
            //mediator.AddScore(score);

            if(other is PlayerBullet||other is PlayerBulletEx)
            {
                hp = hp - 1;
                if (hp < 0)
                {
                    Death();
                }
                mediator.AddScore(100);//得点追加
                isDeadFlag = true;
             }
            else if(other is Player)
            {
                isDeadFlag = true;
            }
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
        private void Death()
        {
            isDeadFlag = true;
            mediator.AddActor(new BurstEffect(position, mediator));
            mediator.AddActor(new zako3(mediator));
            mediator.AddActor(new zako3(mediator));
        }
    }
}
