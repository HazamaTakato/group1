using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;//vector2用
using Microsoft.Xna.Framework.Input;//入力処理用
using Microsoft.Xna.Framework.Graphics;

using Oikake.Actor;
using Oikake.Device;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Util;

namespace Oikake
{
    class Zako2 : Character
    {
        private AI ai;
        private Random rnd;
        private Timer Timer;
        private bool isDisplay;
        private int displayCount;
        private int hp = 1;
        private Vector2 velocity;
        private int score = 0;

        public Zako2(IGameMediator mediator, AI ai)
            : base("teki2", mediator)
        {
            this.ai = ai;
            
        }

        public override void Initialize()
        {
            

            //上から下
            velocity = new Vector2 (0, 1f);
           


            var gameDevice = GameDevice.Instance();
            rnd = gameDevice.GetRandom();
            position = new Vector2(rnd.Next(Screen.Width - 64),- 64);

            /*new Vector2(Screen.Width / 2, Screen.Height / 2);*/

        }

        

        

        public override void Update(GameTime gameTime)
        {
            //Aiが考えて決定した位置に
            position = ai.Think(this);
           
        }

        public override void Shutdown()
        {

        }

        public override void Hit(Character other)
        {
            //得点処理
           
            if (ai is AttackAI)
            {
                score = 100;
            }
            mediator.AddScore(score);


            //次のAIを決定
            //AI nextAI = new boundAI();//実体生成
            //mediator.AddActor(new Zako2(mediator, nextAI));
            hp = hp - 1;
            if (hp< 0)
            {
                Death();
            }
            
        }

        private void Death()  //死亡処理
        {
            isDeadFlag = true;
            mediator.AddActor(new BurstEffect(position, mediator));
        }

       




    }

}
