using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;//vector2用
using Microsoft.Xna.Framework.Input;//入力処理用
using Oikake.Device;
using Oikake.Scene;
using Microsoft.Xna.Framework.Content;//リソースへのアクセス
using Microsoft.Xna.Framework.Media;//MP3
using Microsoft.Xna.Framework.Audio;//WAVデータ
using System.Diagnostics;//Assert用
using Oikake.Def;
using Oikake.Util;

namespace Oikake.Actor
{
    class Enemy:Character
    {
        private AI ai;
        private Random rnd;
        private State state;//状態
        private Timer timer;//表示用切り替え時間
        private bool isDisplay;//表示中か？
        private readonly int Impression = 10;//表示回数
        private int displayCount;//表示カウンタ
        private EnemyBullet enemyBullet;

       // private Vector2 position2;//敵の位置
                                  //    private Vector2 position3;
                                  //    private Vector2 position4;

        public Enemy(IGameMediator mediator,AI ai) :base("black",mediator)
        {
            this.ai = ai;
            state = State.Preparation;
            //   position3 = Vector2.Zero;
            //   position4 = Vector2.Zero;
        }

        ///<summary>
        ///初期化メソッド
        ///</summary>
        public override void Initialize()
        {
            //位置を（１００,１００）に設定
            var gameDevice = GameDevice.Instance();
            rnd = gameDevice.GetRandom();
            position = new Vector2(
                rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));

            //初期状態は準備に
            state = State.Preparation;
            //点滅関連
            timer = new CountDownTimer(0.25f);
            isDisplay = true;
            displayCount = Impression;//点滅回数を設定
            //   position3 = new Vector2(300, 300);
            //  position4 = new Vector2(500, 500);
        }
        //  public void Draw(Renderer renderer)
        //  {
        //レンダラーで白玉画像を描画
        //   renderer.DrawTexture("black", position2);
        //  renderer.DrawTexture("black", position3);
        // renderer.DrawTexture("black", position4);
        //  }
        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.Preparation:
                    PreparationUpdate(gameTime);
                    break;
                case State.Alive:
                    AliveUpdate(gameTime);
                    break;
                case State.Dying:
                    DyingUpdate(gameTime);
                    break;
                case State.Dead:
                    DeadUpdate(gameTime);
                    break;
            }
        }
        public override void Shutdown()
        {
        }
        ///<summary>
        ///ヒット通知
        ///</summary>
        ///<param name="other">衝突した相手</param>
        public override void Hit(Character other)
        {
            //ガード節
            if (state != State.Alive)
            {
                return;
            }
            //状態変更
            state = State.Dying;

            int hp = 3;
            if (enemyBullet is EnemyBullet)
            {
                hp = hp - 1;
            }
            else if (hp == 0)
            {
                isDeadFlag = true;
            }

            //得点処理
            int score = 0;
            if(ai is BoundAI)
            {
                score = 100;
            }
            else if(ai is RandomAI)
            {
                score = 50;
            }
            else if(ai is AttackAI)
            {
                score = -50;
                mediator.AddScore(score);
                mediator.AddActor(new Enemy(mediator, ai));
                isDeadFlag = true;
                return;
            }
            mediator.AddScore(score);

            //次のAIを決定
            AI nextAI = new BoundAI();//実体生成
            switch (rnd.Next(2))
            {
                case 0:
                    nextAI = new BoundAI();
                    break;
                case 1:
                    nextAI = new RandomAI();
                    break;
            }
            mediator.AddActor(new Enemy(mediator, nextAI));

            //死亡処理
            //isDeadFlag = true;
            //mediator.AddActor(new BurstEffect(position, mediator));
        }
        private void PreparationUpdate(GameTime gameTime)
        {
            timer.Update(gameTime);
            if (timer.IsTime())
            {
                isDisplay = !isDisplay;//フラグ反転
                displayCount -= 1;
                timer.Initialize();
            }
            if (displayCount == 0)
            {
                state = State.Alive;//生存状態に
                timer.Initialize();
                displayCount = Impression;
                isDisplay = true;
            }
        }
        private void PreparatinDraw(Renderer renderer)
        {
            if (isDisplay)
            {
                base.Draw(renderer);
            }
        }

        private void AliveUpdate(GameTime gameTime)
        {
            position = ai.Think(this);
        }

        private void AliveDraw(Renderer renderer)
        {
            base.Draw(renderer);
        }

        private void DyingUpdate(GameTime gameTime)
        {
            timer.Update(gameTime);
            if (timer.IsTime())
            {
                displayCount -= 1;
                timer.Initialize();
                isDisplay = !isDisplay;
            }
            if (displayCount == 0)
            {
                state = State.Dead;
            }
        }

        private void DyingDraw(Renderer renderer)
        {
            if (isDisplay)
            {
                renderer.DrawTexture(name, position, Color.Red);
            }
            else
            {
                base.Draw(renderer);
            }
        }

        private void DeadUpdate(GameTime gameTime)
        {
            isDeadFlag = true;
            mediator.AddActor(new BurstEffect(position, mediator));
        }

        private void DeadDraw(Renderer renderer)
        {

        }
        public override void Draw(Renderer renderer)
        {
            switch (state)
            {
                case State.Preparation:
                    PreparatinDraw(renderer);
                    break;
                case State.Alive:
                    AliveDraw(renderer);
                    break;
                case State.Dying:
                    DyingDraw(renderer);
                    break;
                case State.Dead:
                    DeadDraw(renderer);
                    break;
            }
        }
    }
}
