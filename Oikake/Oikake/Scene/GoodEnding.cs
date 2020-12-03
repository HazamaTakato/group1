using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Actor.Effects;
using Oikake.Util;

namespace Oikake.Scene
{
    class GoodEnding:IParticleMediator,IScene
    {
        private bool isEndFlag;//死亡フラグ
        private IScene backGroundScene;//背景シーン
        private Sound sound;//サウンド
        private ParticleManager particleManager;//パーティクル管理者
        private ParticleFactory particleFactory;//パーティクル工場
        private Timer timer;//制限時間

        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name="scene"></param>
        public GoodEnding(IScene scene)
        {
            isEndFlag = false;
            backGroundScene = scene;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            //パーティクル関連
            particleManager = new ParticleManager();
            particleFactory = new ParticleFactory(this);
            timer = new CountDownTimer(1f);
        }
        ///<summary>
        ///描画
        ///</summary>
        ///<param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            //シーンごとにrenderer.Begin()～End()を7
            //書いているのに注意
            //背景となるゲームプレイシーン
            backGroundScene.Draw(renderer);

            renderer.Begin();
            renderer.DrawTexture("good", new Vector2(150, 150));//この画像は自分で準備
            particleManager.Draw(renderer);//パーティクル管理者で描画
            renderer.End();
        }

        ///<summary>
        ///生成
        ///</summary>
        ///<param name="name">パーティクル名</param>
        ///<returns>生成されたパーティクル</returns>
        public Particle generate(string name)
        {
            var particle = particleFactory.create(name);
            particleManager.Add(particle);
            return particle;
        }

        ///<summary>
        ///初期化
        ///</summary>
        public void Initialize()
        {
            isEndFlag = false;
            particleManager.Initialize();
        }

        ///<summary>
        ///終了か？
        ///</summary>
        ///<returns></returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }
        ///<summary>
        ///次のシーンは？
        ///</summary>
        ///<returns></returns>
        public Scene Next()
        {
            return Scene.Title;
        }
        ///<summary>
        ///終了
        ///</summary>
        public void Shutdown()
        {
            sound.StopBGM();
        }
        ///<summary>
        ///更新
        ///</summary>
        ///<param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("goodendingbgm");//このおめでとうBGMは自分で準備
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                sound.PlaySE("endingse");
            }
            //パーティクル関連
            //左側で花火
            var random = GameDevice.Instance().GetRandom();
            var angle = MathHelper.ToRadians(random.Next(-100, -80));
            var velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            velocity *= 20.0f;
            var particle = particleFactory.create("Particle");
            particle.SetPosition(new Vector2(50, 500));
            particle.SetVelocity(velocity);
            particleManager.Add(particle);

            //右側で花火
            angle = MathHelper.ToRadians(random.Next(-120, -60));
            velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            velocity *= 15.0f;
            particle = particleFactory.create("ParticleBlue");
            particle.SetPosition(new Vector2(650, 500));
            particle.SetVelocity(velocity);
            particleManager.Add(particle);

            //円の花火
            timer.Update(gameTime);
            if (timer.IsTime())
            {
                timer.Initialize();
                for(int i = 0; i < 100; i++)
                {
                    angle = MathHelper.ToRadians(random.Next(-180, 180));
                    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity *= 10.0f;
                    particle = particleFactory.create("Particle");
                    particle.SetPosition(new Vector2(400, 100));
                    particle.SetVelocity(velocity);
                    particleManager.Add(particle);
                }
            }
            //パーティクル管理者更新
            particleManager.Update(gameTime);
        }
    }
}
